namespace SwitchAPI.Models.Captcha
{
    public class CaptchaService
    {
        public CaptchaService()
        {
            Captchas = new List<long>();
        }
        public List<long> Captchas { get; set; }
    }
}
