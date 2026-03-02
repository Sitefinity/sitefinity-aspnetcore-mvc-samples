using Microsoft.AspNetCore.Mvc;
using PARAGWidgets.Models.PARAGAnswer;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace PARAGWidgets.ViewComponents
{
    /// <summary>
    /// The view component for the PARAGAnswer widget.
    /// </summary>
    [SitefinityWidget(Title = "PARAG answer", Order = 1, Section = "AI search", IconName = "ai-search-sparkle")]
    [ViewComponent(Name = "PARAGAnswer")]
    public class PARAGAnswerViewComponent : ViewComponent
    {
        private readonly IPARAGAnswerModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="PARAGAnswerViewComponent"/> class.
        /// </summary>
        /// <param name="answerModel">The answer model.</param>
        public PARAGAnswerViewComponent(IPARAGAnswerModel answerModel)
        {
            this.model = answerModel;
        }

        /// <summary>
        /// Invokes the PARAGAnswer widget creation.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public virtual async Task<IViewComponentResult> InvokeAsync(IViewComponentContext<PARAGAnswerEntity> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var viewModel = await this.model.InitializeViewModel(context.Entity, this.HttpContext);

            return this.View(context.Entity.SfViewName, viewModel);
        }
    }
}
