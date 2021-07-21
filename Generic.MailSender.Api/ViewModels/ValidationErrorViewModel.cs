namespace Generic.MailSender.Api.ViewModels
{
    public struct ValidationErrorViewModel
    {
        public ValidationErrorViewModel(string propertyName, string result) : this()
        {
            PropertyName = propertyName;
            Result = result;
        }

        public string PropertyName { get; set; }
        public string Result { get; set; }
    }
}
