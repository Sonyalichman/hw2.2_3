using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace hw2
{

    abstract class Worker
    {
        protected string Name;
        public Worker(string Name)
        {
            this.Name = Name;
        }

        protected string Position;
        protected string WorkDay = " ";

        public void ShowInfo()
        {
            Console.WriteLine(Name);
        }
        public void ShowFullInfo()
        {
            this.FillWorkDay();
            Console.WriteLine(Name + " " + Position + " WorkDay: " + WorkDay);

        }
        protected void Call()
        {
            this.WorkDay = this.WorkDay + "Calling ";
        }
        protected void WriteCode()
        {
            this.WorkDay = this.WorkDay + "Writing code... ";
        }
        protected void Relax()
        {
            this.WorkDay = this.WorkDay + "Relaxing ";
        }
        protected abstract void FillWorkDay();
    }
    class Developer : Worker
    {
        public Developer(string WName) : base(WName)
        {
            this.Position = "Розробник";
        }
        protected override void FillWorkDay()
        {
            this.WriteCode();
            this.Call();
            this.Relax();
            this.WriteCode();
        }
    }
    class Manager : Worker
    {
        public Manager(string WName) : base(WName)
        {
            this.Position = "Менеджер";
        }
        private Random random = new Random();
        protected override void FillWorkDay()
        {
            int Rand = random.Next(10);
            for (int i = 0; i < Rand; i++)
            {
                this.Call();
            }
            this.Relax();
            Rand = random.Next(5);
            for (int i = 0; i < Rand; i++)
                this.Call();
        }

    }
    class Team
    {
        protected int NumberOfMembers;
        public int GetNumberOfMembers() { return this.NumberOfMembers; }
        public void SetNumberOfMembers(int n) { this.NumberOfMembers = n; }
        private string Name;
        public Team(string TName)
        {
            this.NumberOfMembers = 0;
            this.Name = TName;
        }
        public List<Worker> People = new List<Worker>();
        public void AddMember(Worker New)
        {
            // New.ShowInfo();
            People.Add(New);
            this.NumberOfMembers++;
        }
        public void ShowTeamInfo()
        {
            Console.WriteLine(this.Name);
            for (int i = 0; i < this.NumberOfMembers; i++)
            {
                People[i].ShowInfo();
            }
        }
        public void ShowTeamFullInfo()
        {
            Console.WriteLine(this.Name);
            for (int i = 0; i < NumberOfMembers; i++)
            {
                People[i].ShowFullInfo();
            }
        }

    }


    internal class Program
    {
        static void UnitTeams(Team team1, Team team2)
        {
            for (int i = 0; i < team2.GetNumberOfMembers(); i++)
            {
                team1.AddMember(team2.People[i]);
            }
            team1.SetNumberOfMembers(team1.GetNumberOfMembers() + team2.GetNumberOfMembers() - 2);
            team2 = null;
        }
        static void Main(string[] args)
        {
            Team Tea = new Team("Tea");
            Developer D = new Developer("John");
            Tea.AddMember(D);
            Manager M = new Manager("Kate");
            Tea.AddMember(M);
            Tea.ShowTeamInfo();
            Team Tea2 = new Team("Tea2");
            Developer D2 = new Developer("Tod");
            Manager M2 = new Manager("July");
            Tea2.AddMember(D2);
            Tea2.AddMember(M2);
            Tea2.ShowTeamInfo();
            UnitTeams(Tea, Tea2);
            Tea.ShowTeamFullInfo();
            Console.ReadKey();
        }
    }
}
