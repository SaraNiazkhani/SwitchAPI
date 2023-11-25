namespace SwitchAPI.Models.Captcha
{
    public class CaptchaGenerator
    {
        public CaptchaGenerator()
        {
            Captchas = new List<CaptchaModels>();
        }
        public List<CaptchaModels> Captchas { get; set; }


    }
}
