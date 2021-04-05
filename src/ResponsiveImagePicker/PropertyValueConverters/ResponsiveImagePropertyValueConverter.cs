using System;
using Newtonsoft.Json;
using ResponsiveImagePicker.Models;
using StackExchange.Profiling.Internal;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace ResponsiveImagePicker.PropertyValueConverters
{
    public class ResponsiveImagePropertyValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(IPublishedPropertyType publishedPropertyType)
        {
            return publishedPropertyType.EditorAlias.Equals("RyanHelmn.ResponsiveImagePicker");
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType publishedPropertyType)
        {
            return PropertyCacheLevel.Snapshot;
        }

        public override Type GetPropertyValueType(IPublishedPropertyType publishedPropertyType)
        {
            return typeof(ResponsiveImage);
        }

        public override object ConvertSourceToIntermediate(IPublishedElement owner,
            IPublishedPropertyType publishedPropertyType, object source, bool preview)
        {
            var sourceString = source?.ToString();
            var responsiveImage = JsonConvert.DeserializeObject<ResponsiveImage>(sourceString);
            var dataTypeConfiguration = JsonConvert.DeserializeObject<ResponsiveImagePickerPreValues>(publishedPropertyType.DataType.Configuration.ToJson());

            if (dataTypeConfiguration == null)
            {
                return responsiveImage;
            }

            responsiveImage.DefaultPictureClasses = dataTypeConfiguration.DefaultPictureClasses;
            responsiveImage.DefaultImageClasses = dataTypeConfiguration.DefaultImageClasses;

            return responsiveImage;
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner,
            IPublishedPropertyType publishedPropertyType, PropertyCacheLevel referenceCacheLevel, object inter,
            bool preview)
        {
            return inter;
        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner,
            IPublishedPropertyType publishedPropertyType, PropertyCacheLevel referenceCacheLevel, object inter,
            bool preview)
        {
            return inter;
        }
    }
}