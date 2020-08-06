using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name for the person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// last name for the person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email address for the person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Cellphone number for the person
        /// </summary>
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Concats First and LAst name
        /// </summary>
        public string FullName
        {
            get
            {
                return $"{ FirstName } { LastName }";
            }
        }
    }
}
