namespace SwitchAPI.Models.Captcha
{
    public class CaptchaModels
    {
        public CaptchaModels(string captchaToken, string captchaAnswer)
        {
            CaptchaToken = captchaToken;
            CaptchaAnswer = captchaAnswer;
            createCaptchaDate = DateTime.Now;
        }

        public string CaptchaToken { get; set; }
        public string CaptchaAnswer { get; set; }
        public DateTime createCaptchaDate { get; set; }
    }
}
