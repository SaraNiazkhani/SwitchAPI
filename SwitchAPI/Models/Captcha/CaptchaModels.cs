namespace SwitchAPI.Models.Captcha
{
    public class CaptchaModels
    {
        public CaptchaModels(string captchaToken, string captchaAnswer)
        {
            CaptchaToken = captchaToken;
            CaptchaAnswer = captchaAnswer;
            GenerationDate = DateTime.Now;
        }

        public string CaptchaToken { get; set; }
        public string CaptchaAnswer { get; set; }
        public DateTime GenerationDate { get; set; }
    }
}
