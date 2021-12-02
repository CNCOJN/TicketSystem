using System;
using System.Text;
using System.Security.Cryptography;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EditedDate { get; set; }

        public void EncryptPassword(string key)
        {
            byte[]? keyBytes = Encoding.UTF8.GetBytes(key).Take(24).ToArray();
            byte[]? passwordBytes = Encoding.UTF8.GetBytes(PassWord ?? throw new PasswordCannotBeNullException());
            TripleDESCryptoServiceProvider tripleDes = new();
            tripleDes.Key = keyBytes;
            tripleDes.Mode = CipherMode.ECB;
            ICryptoTransform? encryptor = tripleDes.CreateEncryptor();
            byte[]? result = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);
            PassWord = Convert.ToBase64String(result);
        }

        public void DecrptPassWord(string key)
        {
            byte[]? keyBytes = Encoding.UTF8.GetBytes(key).Take(24).ToArray();
            TripleDESCryptoServiceProvider tripleDes = new();
            tripleDes.Key = keyBytes;
            tripleDes.Mode = CipherMode.ECB;
            ICryptoTransform? decryptor = tripleDes.CreateDecryptor();
            byte[]? encryptedPasswordByte = Convert.FromBase64String(PassWord ?? throw new PasswordCannotBeNullException());
            byte[]? passwordByte = decryptor.TransformFinalBlock(encryptedPasswordByte, 0, encryptedPasswordByte.Length);
            PassWord = Encoding.UTF8.GetString(passwordByte);
        }
    }
}