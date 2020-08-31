using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeammembers = new List<PersonModel>();
        private readonly ITeamRequester callingForm;

        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            callingForm = caller;

            // CreateSampleData();

            WireUpList();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Tim", LastName = "Terry" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Chi", LastName = "Strom" });

            selectedTeammembers.Add(new PersonModel { FirstName = "Jane", LastName = "Hobs" });
            selectedTeammembers.Add(new PersonModel { FirstName = "Bill", LastName = "Kane" });
        }

        private void WireUpList()
        {
            selectTeamMemberDropdown.DataSource = null;

            selectTeamMemberDropdown.DataSource = availableTeamMembers;
            selectTeamMemberDropdown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeammembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellphoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                selectedTeammembers.Add(p);

                WireUpList();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill all the fields");
            }
        }

        /// <summary>
        /// Validates the Add new member mini form
        /// </summary>
        /// <returns>true/false</returns>
        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                return false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropdown.SelectedItem;
           
            if (p != null)
            {

                availableTeamMembers.Remove(p);
                selectedTeammembers.Add(p);

                WireUpList();
            }

        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeammembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpList();
            }

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeammembers;

            GlobalConfig.Connection.CreateTeam(t);

            callingForm.TeamComplete(t);
            this.Close();
        }
    }
}
