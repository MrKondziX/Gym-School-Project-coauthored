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
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.UsersLogin == usersLogin); // trzeba wydobyć ID z bazy danych, gdyż do działania AUTO_INCREMENT trzeba bazie danych dać ID 0, chyba że ktoś będzie miał lepszy pomysł niż to

            var userKlient = new UsersKlient
            {
                UsersKlientId = 0,
                UsersId = createdUser.UsersId,
                UsersCoachId = 1
            };
            _context.UsersKlients.Add(userKlient);
            await _context.SaveChangesAsync();
            return "Zarejestrowano Pomyślnie";
        }
        public async Task<string> AddCoach(string usersName, string usersSurname, string usersEmail, string usersLogin, string usersPassword)
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
                UsersTypeId = 2
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var createdUser = await _context.Users.FirstOrDefaultAsync(u => u.UsersLogin == usersLogin); // trzeba wydobyć ID z bazy danych, gdyż do działania AUTO_INCREMENT trzeba bazie danych dać ID 0, chyba że ktoś będzie miał lepszy pomysł niż to

            var userCoach = new UsersCoach
            {
                UsersCoachId = 0,
                UsersId = createdUser.UsersId,
                UsersCoachNott = "Trener Personalny"
            };
            _context.UsersCoaches.Add(userCoach);
            await _context.SaveChangesAsync();
            return "Dodano trenera Pomyślnie";
        }

        public async Task<(string result, int? userType, int? UserId)> Login(string usersLogin, string usersPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UsersLogin == usersLogin);
            if (user == null)
                return ("Nie Znaleziono Użytkownika", null, null);

            bool isPassValid = VerifySha256(usersPassword, user.UsersPassword);
            if (!isPassValid)
                return ("Niepoprawne Hasło", null, null);

            return ("Zalogowano Pomyślnie", user.UsersTypeId, user.UsersId);
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
