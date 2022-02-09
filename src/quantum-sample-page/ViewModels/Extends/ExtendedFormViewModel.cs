using Progress.Sitefinity.AspNetCore.Widgets.Models.Form;

namespace Renderer.ViewModels.Extends
{
    public class ExtendedFormViewModel: FormViewModel
    {
        public ExtendedFormViewModel(FormViewModel source)
        {
            this.CssClass = source.CssClass;
            this.FormModel = source.FormModel;
            this.SubmitUrl = source.SubmitUrl;
            this.CustomSubmitAction = source.CustomSubmitAction;
            this.RedirectUrl = source.RedirectUrl;
            this.SuccessMessage = source.SuccessMessage;
            this.Warning = source.Warning;
            this.SkipDataSubmission = source.SkipDataSubmission;
            this.Rules = source.Rules;
            this.HiddenFields = source.HiddenFields;
            this.Attributes = source.Attributes;
        }
        public string Heading { get; set; }
    }
}
