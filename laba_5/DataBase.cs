using System;
using System.Collections.Generic;

namespace laba_5
{
    class DataBase
    {
        private readonly SortedDictionary<string, Admin> _admins = new SortedDictionary<string, Admin>
        { ["AlexShev"] = new Admin("AlexShev", "0123") };

        private readonly SortedDictionary<string, Client> _clients;

        private readonly SortedDictionary<string, Tuple<List<Client>, List<Client>>> _sortedClients;

        // не смог объеденить - ругается что не может преобразовать базовый класс в производные
        public void AddAdmin(Admin admin)
        {
             _admins.Add(admin.Login, admin);   
        }

        public void AddСlient(Client client)
        {
            if (client.MySex == 'Ж' && client.MySex == 'М') throw new Exception("Не определённый пол");

            _clients.Add(client.Login, client);

            ChoesItem(client).Add(client);
        }

        public bool IsAdmin(Admin admin)
        {
            return _admins.ContainsValue(admin);
        }

        public bool IsAdmin(string login, string password)
        {
            return _admins.ContainsKey(login) ? (_admins[login].IsMyPassword(password)) : false;
        }

        public bool IsClient(Client client)
        {
            return _clients.ContainsValue(client);
        }

        public bool IsClient(string login, string password)
        {
            return _clients.ContainsKey(login) ? (_clients[login].IsMyPassword(password)) : false;
        }

        public SortedDictionary<int, List<Client>> FindPiars(Client client)
        {
            var result = new SortedDictionary<int, List<Client>>();
       
            // я не понял, как сделать по убыванию очков
            foreach (var c in ChoesItem(client))
            {
                int tempScore = client.IsAPaerWithoutSexAndLocalization(c);

                if (tempScore > 3) result[tempScore].Add(c);
            }

            // как я понимаю я делю по категориям по баллам, в списке они по добавлению,
            // значит преподчтение тем, кто юзает дольше
            return result;
        }

        public void DeleteAdmin(Admin admin, string password)
        {
            if (admin.IsMyPassword(password))

                _admins.Remove(admin.Login);

            else
                throw new Exception("Вы ввели не тот пароль");
        }

        public void DeleteClientByClient(Client client, string password)
        {
            if (client.IsMyPassword(password))
            {
                DeleteClient(client);
            }
            else
                throw new Exception("Вы ввели не тот пароль");
        }

        public void DeleteClientByAdmin(Admin admin, Client client, string passwordClient)
        {
            if (admin.IsMyPassword(passwordClient))
            {
                DeleteClient(client);
            }
            else
                throw new Exception("Вы ввели не тот пароль");
        }

        public void DeleteClientByAdmin(Admin admin, string passwordAdmin, string loginClient)
        {
            if (admin.IsMyPassword(passwordAdmin))
            {
                DeleteClient(_clients[loginClient]);
            }
            else
                throw new Exception("Вы ввели не тот пароль");
        }

        private void DeleteClient(Client client)
        {
            _clients.Remove(client.Login);

            ChoesItem(client).Remove(client);
        }

        private List<Client> ChoesItem(in Client client)
        {
            return (client.MySex == 'М') ? _sortedClients[client.MyCity].Item2 : _sortedClients[client.MyCity].Item1;
        }
    }
}