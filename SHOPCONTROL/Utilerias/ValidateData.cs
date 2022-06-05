namespace SHOPCONTROL.Utilerias
{
    public class ValidateData
    {
        public static  bool IsValidEmail(string eMail)
        {
            bool Result = false;

            try
            {
                // int count = eMail.LastIndexOf("@");
                // int count2 = eMail.LastIndexOf(".");
                var eMailValidator = new System.Net.Mail.MailAddress(eMail);
                Result = (eMail.LastIndexOf(".") > eMail.LastIndexOf("@"));


                // generate unique key to validate the email



            }
            catch
            {
                Result = false;
            };

            return Result;
        }

    }
}
