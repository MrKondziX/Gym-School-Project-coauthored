using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Siłownia
{
    public class AuthService
    {
        private readonly GymAppDbContext _context = new();

        public async Task<string> Register(string usersName, string usersSurname, string usersEmail, string usersLogin, string usersPassword)
        {
            if (await _context.Users.AnyAsync(u => u.UsersLogin == usersLogin))
                return "Zajęty Login";

            var user = new User
            {
                UsersId = 0,
                UsersName = usersName,
                UsersSurname = usersSurname,
                UsersEmail = usersEmail,
                UsersLogin = usersLogin,
                UsersPassword = usersPassword,
                UsersTypeId = 3
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "Zarejestrowano Pomyślnie";
        }

        public async Task<(string result, int? userType)> Login(string usersLogin, string usersPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UsersLogin == usersLogin);
            if (user == null)
                return ("Nie Znaleziono Użytkownika", null);

            bool isPassValid = VerifySha256(usersPassword, user.UsersPassword);
            if (!isPassValid)
                return ("Niepoprawne Hasło", null);

            return ("Zalogowano Pomyślnie", user.UsersTypeId);
        }

        private static bool VerifySha256(string plainText, string hashFromDb)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            var computedHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            return computedHash == hashFromDb.ToLower();
        }

    }
}
