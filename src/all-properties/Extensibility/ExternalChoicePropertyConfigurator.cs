using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace all_properties.Extensibility
{
    public class ExternalChoicePropertyConfigurator : IPropertyConfigurator
    {
        public virtual void ProcessPropertyMetadataContainer(PropertyDescriptor descriptor, PropertyMetadataContainerDto propertyContainer, string componentName)
        {
            foreach (Attribute attr in descriptor.Attributes)
            {
                ProcessConfigurationExternalDataChoiceAttribute(attr, propertyContainer);
            }
        }

        private static void ProcessConfigurationExternalDataChoiceAttribute(Attribute attribute, PropertyMetadataContainerDto propertyContainer)
        {
            var externalChoiceAttr = attribute as ExternalDataChoiceAttribute;
            if (externalChoiceAttr != null)
            {
                var choices = FetchChoices();
                var serializedChoices = JsonConvert.SerializeObject(choices);
                propertyContainer.Properties.Add($"{WidgetMetadataConstants.Prefix}_Choices", serializedChoices);

                if (string.Equals(propertyContainer.Type, KnownFieldTypes.ChoiceMultiple))
                {
                    propertyContainer.Properties.Add($"{WidgetMetadataConstants.Prefix}_Choices_AllowMultiple", bool.TrueString);
                    propertyContainer.Type = "multipleChoices";
                }

                var choiceKey = $"{WidgetMetadataConstants.Prefix}_Choice_Choices";
                if (propertyContainer.Properties.ContainsKey($"{WidgetMetadataConstants.Prefix}_Choice_Choices"))
                {
                    propertyContainer.Properties[choiceKey] = serializedChoices;
                }
                else
                {
                    propertyContainer.Properties.Add(choiceKey, serializedChoices);
                }
            }
        }

        private static ChoiceValueDto[] FetchChoices()
        {
            var dynamicChoices = new ChoiceValueDto[]
            {
                new ChoiceValueDto("Option 1", "Option 1"),
                new ChoiceValueDto("Option 2", "Option 2"),
                new ChoiceValueDto("Option 3", "Option 3")
            };

            return dynamicChoices;
        }
    }
}
