using System;
using System.Collections.Generic;
using System.ComponentModel;
using Renderer.Client;
using Newtonsoft.Json;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Dto;

namespace Renderer.Attributes
{
    internal class ExternalPropertyConfigurator : IPropertyConfigurator
    {
        private INativeChatClient nativeChatClient;

        public ExternalPropertyConfigurator(INativeChatClient nativeChatClient)
        {
            this.nativeChatClient = nativeChatClient;
        }

        public virtual void ProcessPropertyMetadataContainer(PropertyDescriptor descriptor, PropertyMetadataContainerDto propertyContainer, string componentName)
        {
            foreach (Attribute attr in descriptor.Attributes)
            {
                ProcessConfigurationExternalDataChoiceAttribute(attr, propertyContainer);
            }
        }

        private void ProcessConfigurationExternalDataChoiceAttribute(Attribute attribute, PropertyMetadataContainerDto propertyContainer)
        {
            var externalChoiceAttr = attribute as ExternalDataChoiceAttribute;
            if (externalChoiceAttr != null)
            {
                var serializedChoices = FetchChoices();
                propertyContainer.Properties.Add($"{WidgetMetadataConstants.Prefix}_Choices", serializedChoices);

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

        private string FetchChoices()
        {
            var bots = this.nativeChatClient.Bots().Result;
            var choices = new List<ChoiceValueDto>() { new ChoiceValueDto("Select", "") };

            foreach (var bot in bots)
            {
                choices.Add(new ChoiceValueDto(bot.Name, bot.Id));
            }

            return JsonConvert.SerializeObject(choices);
        }

        private const string externalUrl = "https://api.nativechat.com/v1/";
    }
}