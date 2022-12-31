using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR57_2020_POP2021.Entities
{
    [Serializable]
    public class RegisteredUser
    {
        private int _id;
        private string name;
        private string surname;
        private string jmbg;
        private EGender gender;
        private int address_id;
        private string email;
        private string password;
        private ERole role;
        private bool active;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string JMBG
        {
            get { return jmbg; }
            set { jmbg = value; }
        }

        public int Address_ID
        {
            get { return address_id; }
            set { address_id = value; }
        }

        public EGender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public ERole Role
        {
            get { return role; }
            set { role = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public RegisteredUser() { }

        public override string ToString()
        {
            return $"{nameof(ID)}: {ID}, {nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}," +
                $" {nameof(Password)}: {Password}, {nameof(Email)}: {Email}, {nameof(JMBG)}: {JMBG}, {nameof(Address_ID)}: {Address_ID}, {nameof(Gender)}: {Gender}, {nameof(Role)}: {Role}, {nameof(Active)}: {Active}";
        }

        public RegisteredUser Clone()
        {
            RegisteredUser copy = new RegisteredUser();
            copy.Name = Name;
            copy.Surname = Surname;
            copy.Password = Password;
            copy.Email = Email;
            copy.JMBG = JMBG;
            copy.Address_ID = Address_ID;
            copy.Gender = Gender;
            copy.Role = Role;
            copy.Active = Active;

            return copy;
        }

    }
}
