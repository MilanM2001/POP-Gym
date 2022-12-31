using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using SR57_2020_POP2021.Services;

namespace SR57_2020_POP2021.Entities
{
    public sealed class Util
    {
        public static String CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=POP2021DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private static readonly Util instance = new Util();
        private IUserService userService;
        private IInstructorService instructorService;
        private IAttendeeService attendeeService;
        private IWorkoutService workoutService;
        private IAdministratorService administratorService;
        private IAddressService addressService;
        private IFitnessCentreService fitnessCentreService;

        private Util()
        {
            userService = new UserService();
            instructorService = new InstructorService();
            attendeeService = new AttendeeService();
            workoutService = new WorkoutService();
            administratorService = new AdministratorService();
            addressService = new AddressService();
            fitnessCentreService = new FitnessCentreService();
        }

        static Util() { }

        public static Util Instance
        {
            get
            {
                return instance;
            }
        }

        public ObservableCollection<RegisteredUser> Users { get; set; }
        public ObservableCollection<Instructor> Instructors { get; set; }
        public ObservableCollection<Attendee> Attendees { get; set; }
        public ObservableCollection<Administrator> Administrators { get; set; }
        public ObservableCollection<Workout> Workouts { get; set; }
        public ObservableCollection<Address> Addresses { get; set; }
        public ObservableCollection<FitnessCentre> FitnessCentres { get; set; }


        //Nisu potrebne sada
        public Address FindAddress(int id)
        {
            foreach (Address address in Addresses)
            {
                if (address.ID == id && !address.Active)
                {
                    return address;
                }
            }
            return null;
        }

        public Instructor FindInstructors(int id)
        {
            foreach (Instructor instructor in Instructors)
            {
                if (instructor.ID == id)
                {
                    return instructor;
                }
            }
            return null;
        }

        public RegisteredUser FindUser(String JMBG) 
        {
            RegisteredUser registeredUser = new RegisteredUser();
            SqlConnection con = new SqlConnection(Util.CONNECTION_STRING);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                String sql = "SELECT * FROM users WHERE JMBG=" + JMBG;
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    registeredUser.ID = reader.GetInt32(0);
                    registeredUser.Name = reader.GetString(1);
                    registeredUser.Surname = reader.GetString(2);
                    registeredUser.JMBG = reader.GetString(3);
                    registeredUser.Gender = (EGender)Enum.Parse(typeof(EGender), reader.GetString(4), true);
                    registeredUser.Address_ID = reader.GetInt32(5);
                    registeredUser.Email = reader.GetString(6);
                    registeredUser.Password = reader.GetString(7);
                    registeredUser.Role = (ERole)Enum.Parse(typeof(ERole), reader.GetString(8), true);
                    registeredUser.Active = reader.GetBoolean(9);
                };
                reader.Close();
            }
            return registeredUser;
        }


        public Attendee FindAttendees(int id)
        {
            foreach (Attendee attendee in Attendees)
            {
                if (attendee.ID == id)
                {
                    return attendee;
                }
            }
            return null;
        }

        public class UserDetails
        {
            private static string jmbg;

            public static string JMBG
            {
                get
                {
                    return jmbg;
                }
                set
                {
                    jmbg = value;
                }
            }
        }

        public int SaveEntity(Object obj)
        {
            if (obj is RegisteredUser)
            {
                return userService.SaveUser(obj);
            }
            else if (obj is Instructor)
            {
                return instructorService.SaveUser(obj);
            }
            else if (obj is Administrator)
            {
                return administratorService.SaveUser(obj);
            }
            else if (obj is Attendee)
            {
                return attendeeService.SaveUser(obj);
            }
            else if (obj is Workout)
            {
                return workoutService.SaveWorkouts(obj);
            }
            else if (obj is Address)
            {
                return addressService.SaveAddress(obj);
            }
            else if (obj is FitnessCentre)
            {
                return fitnessCentreService.SaveFitnessCentre(obj);
            }
            return -1;
        }

        public void UpdateEntity(Object obj)
        {
            if (obj is RegisteredUser)
            {
                userService.UpdateUser(obj);
            }
            if (obj is Instructor)
            {
                instructorService.UpdateInstructor(obj);
            }
            else if (obj is Administrator)
            {
                administratorService.UpdateAdministrator(obj);
            }
            else if (obj is Attendee)
            {
                attendeeService.UpdateAttendee(obj);
            }
            else if (obj is Workout)
            {
                workoutService.UpdateWorkout(obj);
            }
            else if (obj is Address)
            {
                addressService.UpdateAddress(obj);
            }
            else if (obj is FitnessCentre)
            {
                fitnessCentreService.UpdateFitnessCentre(obj);
            }
            
        }

        public void ReadEntity(string filename)
        {
            if (filename.Contains("Users"))
            {
                userService.ReadUsers();
            }
            else if (filename.Contains("Instructors"))
            {
                instructorService.ReadUsers();
            }
            else if (filename.Contains("Attendees"))
            {
                attendeeService.ReadUsers();
            }
            else if (filename.Contains("Administrators"))
            {
                administratorService.ReadUsers();
            }
            else if (filename.Contains("Workouts"))
            {
                workoutService.ReadWorkouts();
            }
            else if (filename.Contains("Address"))
            {
                addressService.ReadAddress();
            }
            else if (filename.Contains("FitnessCentre"))
            {
                fitnessCentreService.ReadFitnessCentre();
            }
        }

        public void DeleteUser(int id)
        {
            userService.DeleteUser(id);
        }

        public void DeleteWorkout(int id)
        {
            workoutService.DeleteWorkouts(id);
        }

        public void DeleteAddress(int id)
        {
            addressService.DeleteAddress(id);
        }

        public void DeleteFitnessCentre(int id)
        {
            fitnessCentreService.DeleteFitnessCentre(id);
        }

    }

}