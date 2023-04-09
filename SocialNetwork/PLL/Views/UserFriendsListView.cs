using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class UserFriendsListView
    {
        UserService userService;
        public UserFriendsListView(UserService userService)
        {
            this.userService = userService;
        }
        public void Show(User user)
        {
            Console.WriteLine();
            Console.WriteLine("Ваши друзья:");

            if (user.Friends.Count() == 0)
            {
                Console.WriteLine("У вас пока еще нет друзей.");
                Console.WriteLine();
                ShowFriendsRequests(user);
                return;
            }

            user.Friends.ToList().ForEach(friend => Console.WriteLine("Имя: {0}, Фамилия: {1}", friend.FirstName, friend.LastName));
            Console.WriteLine();
            ShowFriendsRequests(user);
        }

        void ShowFriendsRequests(User user)
        {
            var possibleFriends = userService.GetPossibleFriends(user.Id);
            if (possibleFriends.Count() != 0)
            {
                Console.WriteLine("Пользователи не в вашем списке друзей, добавившие Вас в друзья:");
                possibleFriends.ToList().ForEach(friend => Console.WriteLine("Имя: {0}, Фамилия: {1}, Почтовый адрес: {2}", friend.FirstName, friend.LastName, friend.Email));
                Console.WriteLine();
            }
        }
    }
}
