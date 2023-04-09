using SocialNetwork.BLL.Exceptions;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.PLL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialNetwork.PLL.Views
{
    public class AddFriendView
    {
        UserService userService;
        public AddFriendView(UserService userService)
        {
            this.userService = userService;
        }
        public void Show(User user)
        {
            try
            {
                var addingFriendData = new AddingFriendData();

                Console.WriteLine("Введите почтовый адрес пользователя, которого хотите добавить в друзья:");

                addingFriendData.FriendEmail = Console.ReadLine();
                addingFriendData.UserId = user.Id;

                userService.AddFriend(addingFriendData);

                SuccessMessage.Show("Друг добавлен!");
            }
            catch (UserNotFoundException)
            {
                AlertMessage.Show("Пользователь не найден!");
            }
            catch (Exception)
            {
                AlertMessage.Show("Произошла ошибка при добавлении пользователя в друзья!");
            }
        }
    }
}
