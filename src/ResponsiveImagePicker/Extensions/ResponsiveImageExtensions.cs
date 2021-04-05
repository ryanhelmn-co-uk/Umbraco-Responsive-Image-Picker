using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResponsiveImagePicker.Models;
using Umbraco.Core;

namespace ResponsiveImagePicker.Extensions
{
    public static class ResponsiveImageExtensions
    {
        public static IHtmlString GetPictureTag(this ResponsiveImage responsiveImage, string pictureClasses = "",
            string imageClasses = "", string alternativeText = "", int imageQuality = 80)
        {
            var pictureTag = new TagBuilder("picture");
            pictureTag.MergeAttribute("class", pictureClasses.IsNullOrWhiteSpace() ? responsiveImage.DefaultPictureClasses : $"{responsiveImage.DefaultPictureClasses} {pictureClasses}");

            if (responsiveImage.Crops == null || !responsiveImage.Crops.Any())
            {
                return new HtmlString(pictureTag.ToString());
            }

            var crops = responsiveImage.Crops.OrderByDescending(x => x.BreakPoint).ToList();

            // Skip smallest crop
            foreach (var crop in crops.Take(responsiveImage.Crops.Count - 1))
            {
                var sourceTag = new TagBuilder("source");
                sourceTag.MergeAttribute("media", $"(min-width: {crop.BreakPoint}px)");
                sourceTag.MergeAttribute("srcset",
                    crop.Coordinates != null
                        ? $"{responsiveImage.Url}?crop={crop.Coordinates.X1},{crop.Coordinates.Y1},{crop.Coordinates.X2},{crop.Coordinates.Y2}&cropmode=percentage&mode=crop&width={crop.Width}&height={crop.Height}&quality={imageQuality}"
                        : $"{responsiveImage.Url}?center={responsiveImage.FocalPoint.Top},{responsiveImage.FocalPoint.Left}&cropmode=percentage&mode=crop&width={crop.Width}&height={crop.Height}&quality={imageQuality}");
                pictureTag.InnerHtml += sourceTag;
            }

            var smallestCrop = crops.Last();
            var imageTag = new TagBuilder("img");
            imageTag.MergeAttribute("src",
                smallestCrop.Coordinates != null
                    ? $"{responsiveImage.Url}?crop={smallestCrop.Coordinates.X1},{smallestCrop.Coordinates.Y1},{smallestCrop.Coordinates.X2},{smallestCrop.Coordinates.Y2}&cropmode=percentage&mode=crop&width={smallestCrop.Width}&height={smallestCrop.Height}&quality={imageQuality}"
                    : $"{responsiveImage.Url}?center={responsiveImage.FocalPoint.Top},{responsiveImage.FocalPoint.Left}&cropmode=percentage&mode=crop&width={smallestCrop.Width}&height={smallestCrop.Height}&quality={imageQuality}");
            imageTag.MergeAttribute("alt", alternativeText);
            imageTag.MergeAttribute("class", imageClasses.IsNullOrWhiteSpace() ? responsiveImage.DefaultImageClasses : $"{responsiveImage.DefaultImageClasses} {imageClasses}");

            pictureTag.InnerHtml += imageTag;
            return new HtmlString(pictureTag.ToString());
        }
    }
}