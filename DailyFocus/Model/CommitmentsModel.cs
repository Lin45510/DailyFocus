using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DailyFocus.DataBase.DAO;
using DailyFocus.View;
using DailyFocus.ViewModel;
using SQLite;

namespace DailyFocus.Model
{
    [Table("commitments")]
    public class CommitmentsModel
    {
        #region DataBase Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string DayofWeek { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Local { get; set; }

        #endregion

        #region Properties

        [Ignore]
        public List<CommitmentsModel> CommitmentsOnDate { get; set; }
        public ICommand Command;
        private readonly CommitmentsDAO commitmentsDAO = new();

        #endregion

        #region Functions

        public async Task<ObservableCollection<CommitmentsModel>> GroupCommitmentsbyDate()
        {

            ObservableCollection<CommitmentsModel> commitmentsByDate = new();

            // Create a list of pets.
            List<CommitmentsModel> commList = await commitmentsDAO.GetItemsAsync();

            // Group the pets using Age as the key value
            // and selecting only the pet's Name for each value.
            IEnumerable<IGrouping<string, CommitmentsModel>> query =
                commList.GroupBy(x => x.Date, x => x).OrderBy(x => DateTime.Parse(x.Key, new CultureInfo("pt-BR")));

            foreach (IGrouping<string, CommitmentsModel> commitments in query)
            {
                List<CommitmentsModel> commitmentsOnDate = new();

                foreach (CommitmentsModel name in commitments)
                {
                    commitmentsOnDate.Add(new CommitmentsModel
                    {
                        Id = name.Id,
                        Status = name.Status,
                        Name = name.Name,
                        Date = name.Date,
                        Local = name.Local,
                        StartTime = name.StartTime,
                        EndTime = name.EndTime,
                        DayofWeek = DayofWeek,
                    });
                }
                commitmentsByDate.Add(
                    new CommitmentsModel
                    {
                        Date = commitments.Key,
                        DayofWeek = DateOnly.ParseExact(commitments.Key, "dd/MM/yyyy").ToString("dddd", new CultureInfo("pt-BR")),
                        CommitmentsOnDate = commitmentsOnDate
                    });
            }
            return commitmentsByDate;
        }

        public async Task<ObservableCollection<CommitmentsModel>> GroupCommitmentsbyDateTime()
        {

            ObservableCollection<CommitmentsModel> commitmentsByDate = new();

            List<CommitmentsModel> commList = await commitmentsDAO.GetItemsAsync();

            IEnumerable<IGrouping<string, CommitmentsModel>> query =
                commList.GroupBy(x => x.Date, x => x).OrderBy(x => DateTime.Parse(x.Key, new CultureInfo("pt-BR")));

            foreach (IGrouping<string, CommitmentsModel> petGroup in query)
            {
                List<CommitmentsModel> commitmentsOnDate = new();

                foreach (CommitmentsModel name in petGroup)
                {
                    if (name.StartTime != "")
                    {
                        commitmentsOnDate.Add(new CommitmentsModel
                        {
                            Id = name.Id,
                            Status = name.Status,
                            Name = name.Name,
                            Date = name.Date,
                            Local = name.Local,
                            StartTime = name.StartTime,
                            EndTime = name.EndTime,
                            DayofWeek = DayofWeek,
                        });
                    }
                }
                List<CommitmentsModel> commitmentsbyDateTime = commitmentsOnDate.OrderBy(x => TimeOnly.Parse(x.StartTime)).ToList();

                commitmentsByDate.Add(
                    new CommitmentsModel
                    {
                        Date = petGroup.Key,
                        DayofWeek = DateOnly.ParseExact(petGroup.Key, "dd/MM/yyyy").ToString("dddd", new CultureInfo("pt-BR")),
                        CommitmentsOnDate = commitmentsbyDateTime
                    });
            }
            return commitmentsByDate;
        }

        public async Task<ObservableCollection<CommitmentsModel>> GetCommitmentsOnDate(string Date, bool Time = true)
        {
            ObservableCollection<CommitmentsModel> CommitmentsOnDate = new();
            List<CommitmentsModel> commitments = new();

            List<CommitmentsModel> commList = await commitmentsDAO.GetItemsAsync();

            if (Time)
            {
                commitments = commList.Where(x => x.Date == Date && x.StartTime != null && x.StartTime != "").ToList();
            }
            else
            {
                commitments = commList.Where(x => x.Date == Date && x.StartTime == "").ToList();
            }

            foreach (CommitmentsModel commitment in commitments)
            {
                CommitmentsOnDate.Add(commitment);
            }

            return CommitmentsOnDate;
        }

        public async Task<CommitmentsModel> GetCommitment(int id)
        {
            List<CommitmentsModel> Commit = await commitmentsDAO.GetItemsAsync();

            CommitmentsModel Commitment = Commit.Find(x => x.Id == id);

            return Commitment;
        }

        public async Task Delete(CommitmentsModel commitments)
        {
            await commitmentsDAO.DeleteItemAsync(commitments);
        }

        public async Task Edit(CommitmentsModel commitment)
        {
            await commitmentsDAO.SaveItemAsync(commitment);
        }

        public async Task EditPopUp(CommitmentsModel commitments, ShellVM shellVM)
        {
            bool time = !(commitments.StartTime == "" || commitments.EndTime == "");
            await Shell.Current.Navigation.PushAsync(new NewCommitment(new()
            {
                Id = commitments.Id,
                status = commitments.Status,
                Commitment = commitments.Name,
                date = commitments.Date,
                Local = commitments.Local,
                Time = time,
                Starttime = time ? TimeSpan.Parse(commitments.StartTime) : new TimeSpan(12, 0, 0),
                Endtime = time ? TimeSpan.Parse(commitments.EndTime) : new TimeSpan(12, 0, 0),
                shellVM = shellVM
            }));
        }

        #endregion
    }
}
