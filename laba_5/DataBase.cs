using System;
using System.Collections.Generic;
using System.Linq;

namespace laba_5
{
    public class DataBase
    {
        private readonly SortedDictionary<string, Admin> _admins = new SortedDictionary<string, Admin>
        { ["AlexShev"] = new Admin("AlexShev", "0123") };

        private readonly SortedDictionary<string, Client> _clients = new SortedDictionary<string, Client>();

        private readonly SortedDictionary<string, ClientTonw> _sortedClients = new SortedDictionary<string, ClientTonw>();

        private class ClientTonw
        {
            private readonly List<Client> _female = new List<Client>();

            private readonly List<Client> _masculine = new List<Client>();

            public List<Client> ChoesPartTounBySex(Client client, bool oppositeSex = false)
            {
                bool isMasculine = client.MySex.MySex == Gender.Sex.masculine;

                return (oppositeSex) ? (isMasculine) ? _female : _masculine : (isMasculine) ? _masculine : _female;
            }
        }

        private List<Client> ChoesToun(Client client, bool oppositeSex = false)
        {
            if (!_sortedClients.ContainsKey(client.MyCity))
            {
                _sortedClients.Add(client.MyCity, new ClientTonw());
            }

            return _sortedClients[client.MyCity].ChoesPartTounBySex(client, oppositeSex);
        }

        // не смог объеденить - ругается что не может преобразовать базовый класс в производные
        public void AddAdmin(Admin admin)
        {
            _admins.Add(admin.Login, admin);
        }

        public void AddСlient(Client client)
        {
            _clients.Add(client.Login, client);

            ChoesToun(client).Add(client);
        }

        public bool IsFreeLoginClients(string login) => !(_clients.ContainsKey(login));

        public bool IsFreeLoginAdmins(string login) => !(_admins.ContainsKey(login));

        public bool IsMyAdmin(Admin admin) => _admins.ContainsValue(admin);

        public bool IsMyClient(string[] loginPassword)
            => _clients.ContainsKey(loginPassword[0]) ? _clients[loginPassword[0]].IsMyPassword(loginPassword[1]) : false;

        public Client GetClient(string[] loginPassword)
            => (IsMyClient(loginPassword)) ? _clients[loginPassword[0]] : throw new Exception("Такого пользователя нет в базе данных");

        // бунтовал, что не может привести к этому виду, надеюсь правильно сделал
        public IOrderedEnumerable<KeyValuePair<int, List<Client>>> FindPiars(string[] loginPassword, int maxAgeDif = 5)
        {
            var result = new SortedDictionary<int, List<Client>>();

            Client client = GetClient(loginPassword);

            foreach (var c in ChoesToun(client, true))
            {
                int tempScore = client.ScoreIsPaerWithoutSexAndLocalization(c, maxAgeDif);

                if (tempScore > 3)
                {
                    if (!result.ContainsKey(tempScore))
                    {
                        result.Add(tempScore, new List<Client>());
                    }

                    result[tempScore].Add(c);
                }
            }

            return result.OrderByDescending(kvp => kvp.Key);
        }

        public void DeleteAdmin(Admin admin)
        {
            if (IsMyAdmin(admin))
            {
                _admins.Remove(admin.Login);
            }
            else
            {
                throw new Exception("Такого админа нет или неправильный пароль");
            }
        }

        public void DeleteClientByClient(string[] loginPassword)
        {
            DeleteClient(GetClient(loginPassword));
        }

        public void DeleteClientByAdmin(Admin admin, string loginClient)
        {
            if (IsMyAdmin(admin))
            {
                DeleteClient(_clients[loginClient]);
            }
            else
            {
                throw new Exception("Вы не админ");
            }
        }

        private void DeleteClient(Client client)
        {
            _clients.Remove(client.Login);

            ChoesToun(client).Remove(client);
        }
    }
}