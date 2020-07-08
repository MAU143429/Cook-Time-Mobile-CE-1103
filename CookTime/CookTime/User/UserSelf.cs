using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CookTime.User
{
    class UserSelf {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Posts { get; set; }
        public JArray Recipes { get; set; }   

        public UserSelf(string name, int age, string email, string password, int followers, int following , int posts, JArray recipes)
        {
            Name = name;
            Age = age;
            Email = email;
            Password = password;
            Followers = followers;
            Following = following;
            Posts = posts;
            Recipes = recipes;
        }
        public void AddRecipe(JObject newRecipe)
        {
            Recipes.Add(newRecipe);
        }
        public void AddFollower()
        {
            Followers += 1;
        }
        public void AddFollowing()
        {
            Following += 1;
        }
        public void AddPost()
        {
            Posts += 1;
        }
        
    }
}
  
    

