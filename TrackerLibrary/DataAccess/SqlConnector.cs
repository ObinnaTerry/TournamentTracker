﻿using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Takes person model and saves it to SQL database.
        /// </summary>
        /// <param name="model">Person model</param>
        /// <returns>Person model</returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString()))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
                return model;

            }
        }

        /// <summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model">CreatePrize model</param>
        /// <returns>The saved model, including the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString()))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
            
        }

        /// <summary>
        /// Takes a team model and saves it to an SQL database. 
        /// </summary>
        /// <param name="model">Team model</param>
        /// <returns>model</returns>
        public TeamModel CreateTeam(TeamModel model)
        {
            using (SqlConnection connection = new SqlConnection(GlobalConfig.CnnString()))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName", model.TeamName);
                
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    p = new DynamicParameters();
                    p.Add("@TeamId", model.Id);
                    p.Add("@PersonId", tm.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", p, commandType: CommandType.StoredProcedure);
                }

                return model;

            }
        }

        /// <summary>
        /// Queries the dtabase and fetched all records from the person table
        /// </summary>
        /// <returns>List of person model</returns>
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using(SqlConnection connection = new SqlConnection(GlobalConfig.CnnString()))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }
    }
}
