namespace SwitchAPI.Models.Captcha
{
    public class CaptchaModels
    {
        public CaptchaModels(string captchaToken, string captchaAnswer)
        {
            CaptchaToken = captchaToken;
            CaptchaAnswer = captchaAnswer;
            
        }

        public string CaptchaToken { get; set; }
        public string CaptchaAnswer { get; set; }
         public DateTime ExpiryTime { get; set; }
        
    }
}
