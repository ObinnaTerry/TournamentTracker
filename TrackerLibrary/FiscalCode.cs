using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TrackerLibrary
{
    class FiscalCode
    {
        [DllImport("FiscalCode.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetFiscalCode")]
        static extern int GetFiscalCode(string BPN, string code, string number, string date, string terminalID, string amount, [MarshalAs(UnmanagedType.LPStr)] StringBuilder fiscalCode, byte[] priKey, int keyLen);
        static void Main(string[] args)
        {

            StringBuilder fiscalCode = new StringBuilder();
            int i;

            string base64Encoded = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAKj84jNlGoNqcMlovu1DtqAtsv+JncnG+rup4AtoHLJO8Yeakc5GwHKhGiIgGytx+afdw67E0hEsHFKNmoWW7z+rZ9/+XML0lnypA1E7AFHuuUzOIonDcN+nswGfJugOAEHIAMdT2z6hyg0JJISIXhdyve2UU5URH+sj51tgF4KzAgMBAAECgYB+aKzMxVSP+Y9lGnW4Bm8WYxkDDm56Ad6NgwWVgsv0lhkW1wk6xLIxDVH/NQUd+qRGbh1zHsgFEHofRs3QIJfWUZ7t0eaUSAb7dcil/hrLM1vwITySlanWmdY+GOtQEAwUhqYQ+dfaGmZyNTMYdzpCK4jrUH8QConqHW+UK0zwAQJBANTrJXsUSauXoflLXKYFME3j8v/ZSut/9rgI6k9whMdvtCepdMLXGu3HAybJCjyJtO5opJWxqLc/Th/gFZ8zBhECQQDLLjGRjB4jbB7hflPLc7r0LhBLtpf7DRYI1Gjx60/qgv59RyhKSTcopWVPyAXFt1f1kf2qhq7OtpAcSoXHvOiDAkAAtU+Im0ardXOqep+yRa4ldC6ovzZKlkBh2FW/MeafKzX0M9IZm9GRHOpbxKtuDB83880oWlDvWxyNapdn0imhAkAiuDcSNw5jYTN5tvuctZ8J4OE5/u7DoL3WLm3sgRh763yPbdxtrUs2r+S7GGQcIDNwrEzbmf4/JqVDoFxyAE7/AkBtdKSlpRuNFAMgeQCY0RDCJZfpGabnuieAWekyHQj0Bnva9b/4X5oniqrhowCQhEAMyrdM750VXjqtJ4FjAUTC";




            byte[] data;

            data = System.Convert.FromBase64String(base64Encoded);
            i = data.Length;

            //String str = System.Text.Encoding.Default.GetString(data);
            //StringBuilder base64Decoded = new StringBuilder(data);


            byte[] byteArray = { 0x31, 0x32, 0x03, 0x04, 0x05 };
            string str = System.Text.Encoding.Default.GetString(byteArray);

            //   StringBuilder pri = new StringBuilder();
            GetFiscalCode("000000001001747472", "000200110000", "00044870", "20200522144635", "010100001134", "00000000000000160.00", fiscalCode, data, data.Length);
        }
    }
}
