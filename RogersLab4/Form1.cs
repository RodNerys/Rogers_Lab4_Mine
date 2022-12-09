using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RogersLab4
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        /*By: Noah Rogers
         *Due: Nov.17, 2022
         *Description: This Program Calculates The total Cost for a trip comprising of 1 to 10 people for 3 possible locations.
         *The program verifies that the user input is correct and displays appropriate error messages if the input is incorrect
         *Once the user submits the booking, a display will show their selected options, the tallied price and an additional option
         *that may or may not be applied based on the selections.
        */

        private void ResetTrip()
        {
            //hide groupbox
            grpTripInformation.Hide();

            //select cuba &  Credit Card radio buttons
            radCuba.Checked = true;
            radCreditCard.Checked = true;

            //clear and foxus people textbox
            txtPeople.Clear();
            txtPeople.Focus();

            //clear price label
            lblPrice.Text = "";

        }

        private void SetFlight()
        {
            if (radFlorida.Checked)
            {
                chkFlight.Checked = false;
            }
            else
            {
                chkFlight.Checked = true;
            }
        }

        private void DisplayMsg(string message, string title)
        {
            MessageBox.Show(message, title);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTrip();
        }

        private void radFlorida_CheckedChanged(object sender, EventArgs e)
        {
            SetFlight();
        }


        private void radCuba_CheckedChanged(object sender, EventArgs e)
        {
            SetFlight();
        }

        private void radMexico_CheckedChanged(object sender, EventArgs e)
        {
            ResetTrip();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            //constants declared
            const double OtherPrice = 2150.50;
            const double MexicoPrice = 2300.79;
            const double Discount = 0.90;

            //varibles
            int People;
            bool validPeople;
            double Price;
            string Applied;

            //Location
            string Location = "";
            if(radMexico.Checked)
                {
                Location = "MEXICO";
                    }
            else if(radFlorida.Checked)
                {
                Location = "FLORIDA";
                    }
            else if(radCuba.Checked)
                {
                Location = "CUBA";
                    }
            else {
                Location = "";
                    }

            if (chkFlight.Checked)
                Applied = "Flight Included";
            if (radCash.Checked)
                Applied = "Cash Discount: Applied";

            //vaildate that an integer is entered
            validPeople = int.TryParse(txtPeople.Text, out People);

            //if an integer is not entered...
            if (validPeople != true)
            {
                //display this message
                DisplayMsg("People must be a whole number", "Input Error");
            }

            //if an integer is outside the required range
            else if (People < 1 || People > 10)
            {
                //display this message
                DisplayMsg("People must be between 1-10", "input error");
            }

            else{

                //if cash is checked
                if (radCash.Checked)
                {
                    //use discount for both cases
                    if (radMexico.Checked)
                    {
                        Price = People * MexicoPrice * Discount;
                    }

                    else
                    {
                        Price = People * OtherPrice * Discount;
                    }

                }

                //if cash is unchecked
                else
                {
                    //calculate both cases without discount
                    if (radMexico.Checked)
                    {
                        Price = People * MexicoPrice;
                    }

                    else
                    {
                        Price = People * OtherPrice;
                    }
                }

                grpTripInformation.Show();
                lblDisplay.Text = "Booked by Noah Rogers\n\n" +
                                  "People: " + People.ToString("D2") +
                                  "\nLocation: " + Location + "\n"
                                  + Applied + "\n"
                                  + "Price: " + Price.ToString("C");
                lblPrice.Text = Price.ToString("C");
                grpBook.Enabled = false;


                switch (People)
                {
                    case 1: case 3:
                        DisplayMessage("Special when booking single or triple.\nBOGO Special - Call 555-1212 to receive another person free!", "Limited Time Offer");
                        break;

                }

            }
                
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //Declare variable Price from label in previous Event
            string Price = lblPrice.Text;

            //Variable Used calling the DisplayMsg Function, reenable booking group and reset the form
            DisplayMsg("Trip booked and paid\nPrice: " + Price, "Confirmation Email Sent");
            grpBook.Enabled = true;
            ResetTrip();

        }
    }
}
