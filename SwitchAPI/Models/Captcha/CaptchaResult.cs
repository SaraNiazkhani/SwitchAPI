namespace SwitchAPI.Models.Captcha
{
    public  class CaptchaResult
    {
        public byte[]? CaptchaImage { get; set; }
        public string? CaptchaToken { get; set; }
    }
}
