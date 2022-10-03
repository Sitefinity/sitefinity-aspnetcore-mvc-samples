using Progress.Sitefinity.RestSdk.Dto;
using Progress.Sitefinity.RestSdk;
using System.Threading.Tasks;
using Xunit;
using Progress.Sitefinity.RestSdk.Clients.Pages.Dto;
using Progress.Sitefinity.RestSdk.Clients.LayoutEditor;
using System.Collections.Generic;
using Progress.Sitefinity.RestSdk.Management;

namespace integrationtests
{
    public class BasicTests : IClassFixture<IntegrationTestsWebApplicationFactory>
    {
        private IntegrationTestsWebApplicationFactory factory;

        public BasicTests(IntegrationTestsWebApplicationFactory factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task ApiTest()
        {
            using (var restClient = this.factory.CreateAuthenticatedRestClient())
            {
                var item = await restClient.CreateItem(new NewsDto()
                {
                    Title = "test"
                });

                var response = await restClient.GetItems<NewsDto>(x => x.Id == item.Id);
                Assert.Single(response.Items);

                // items are deleted automatically. CleanUpRestClient handles that in its Dispose method
            }
        }

        [Fact]
        public async Task TestFrontendRenderContentBlock()
        {
            using (var restClient = this.factory.CreateAuthenticatedRestClient())
            {
                // Arrange
                var rendererPage = new PageNodeDto()
                {
                    Title = "PageDraft",
                    TemplateName = "NetCore.Default", // {{Renderer}}.{{TemplateName}}, Default is provided always
                };

                rendererPage = await restClient.Pages().CreateItem(rendererPage);
                await restClient.Pages().Lock(rendererPage);
                await restClient.Pages().CreateWidget(rendererPage, new AddWidgetArgs()
                {
                    Name = "SitefinityContentBlock",
                    PlaceholderName = "Body",
                    Properties = new Dictionary<string, string>()
                    {
                        { "Content", "Hello Tests !" }
                    },
                });

                await restClient.PublishItem(rendererPage);
                
                // fetch again to get actual live URL
                rendererPage = await restClient.RefreshItem(rendererPage);

                // test the output
                using (var defaultClient = this.factory.CreateClient())
                {
                    var htmlContent = await defaultClient.GetStringAsync(rendererPage.ViewUrl);
                    Assert.Contains("Hello Tests !", htmlContent);
                }
            }
        }

        [Fact]
        public async Task TestFrontendRenderNestedContentBlock()
        {
            using (var restClient = this.factory.CreateAuthenticatedRestClient())
            {
                // Arrange
                var rendererPage = new PageNodeDto()
                {
                    Title = "PageDraft",
                    TemplateName = "NetCore.Default", // {{Renderer}}.{{TemplateName}}, Default is provided always
                };

                rendererPage = await restClient.Pages().CreateItem(rendererPage);
                await restClient.Pages().Lock(rendererPage);
                var sectionWidget = await restClient.Pages().CreateWidget(rendererPage, new AddWidgetArgs()
                {
                    Name = "SitefinitySection",
                    PlaceholderName = "Body",
                });

                var firstContentBlock = await restClient.Pages().CreateWidget(rendererPage, new AddWidgetArgs()
                {
                    Name = "SitefinityContentBlock",
                    PlaceholderName = "Column1", // default name when the section widget is in a single column
                    Properties = new Dictionary<string, string>()
                    {
                        { "Content", "Hello Tests First !" }
                    },
                    ParentPlaceholderKey = sectionWidget.Key,
                });

                await restClient.Pages().CreateWidget(rendererPage, new AddWidgetArgs()
                {
                    Name = "SitefinityContentBlock",
                    PlaceholderName = "Column1", // default name when the section widget is in a single column
                    Properties = new Dictionary<string, string>()
                    {
                        { "Content", "Hello Tests Second !" }
                    },
                    ParentPlaceholderKey = sectionWidget.Key,
                    SiblingKey = firstContentBlock.Key,
                });

                await restClient.PublishItem(rendererPage);
                
                // fetch again to get actual live URL
                rendererPage = await restClient.RefreshItem(rendererPage);

                // test the output
                using (var defaultClient = this.factory.CreateClient())
                {
                    var htmlContent = await defaultClient.GetStringAsync(rendererPage.ViewUrl);
                    Assert.Contains("Hello Tests First !", htmlContent);
                    Assert.Contains("Hello Tests Second !", htmlContent);

                    var indexOfFirstText = htmlContent.IndexOf("Hello Tests First !");
                    var indexOfSecondtext = htmlContent.IndexOf("Hello Tests Second !");
                    
                    Assert.True(indexOfSecondtext > indexOfFirstText);
                }
            }
        }
    }
}