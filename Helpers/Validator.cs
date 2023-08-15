namespace EcommerceConsole.Helpers
{
    public class VALIDATOR
    {
        public static bool VALIDATE(List<string> INPUT)
        {
            var VALID = false;

            foreach (var input in INPUT)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    VALID = false;
                }
                else
                {
                    VALID = true;
                }
            }
            return VALID;
        }
    }
}