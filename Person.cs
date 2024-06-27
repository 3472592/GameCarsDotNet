
namespace LabProj
{
    /// <summary>
    /// Person class is responsible for Person's - 
    /// Name, Person Bet, Cash, text update on radio button or label if certain condition meets expectations.
    /// Also this class has a method that is responsible for clearing out all bets, mostly used after race.
    /// Also there is a PlaceBet method it takes betted amount and # of racer that bet is going on to.
    /// Also Person class is responsible for checking after race, if Bet was placed so we can pay winner or take away money from looser.
    /// </summary>
    public class Person
    {
        // this variable is keeping name of person.
        public string Name;

        // keeps Person's bet.
        public Bet? PersonBet = null;

        // keeps Person's cash amount.
        public int Cash;

        // two fields below are used for Person's GUI controls on the form.
        public RadioButton PersonRadioButton;
        public Label PersonLabel;

        public Person(string name, int cash, RadioButton personRadioButton, Label personLabel)
        {
            Name = name;
            Cash = cash;
            PersonRadioButton = personRadioButton;
            PersonLabel = personLabel;
        }

        #pragma warning disable CS8618 
        // Non-nullable field must contain a non-null value when exiting constructor.
        // Consider declaring as nullable.
        public Person()
        {
        }

        public void LabelsRadioBtnsUpdate()
        {
            if (PersonBet == null) // if bet was not placed.
                PersonLabel.Text = Name + " hasn't placed any bets";

            else// use UpdateLabel method if, if condition was not met.
                PersonLabel.Text = PersonBet.BettingParlorUpdateLabel();

            if (Cash >= 5) 
                PersonRadioButton.Text = Name + " has $" + Cash;
            else if (Cash <= 4 && Cash != 0)
            {
                PersonRadioButton.Text = Name + " has $" + Cash;
                PersonLabel.Text = Name + " does not have enough to place a bet";
                PersonRadioButton.Enabled = false;
                PersonRadioButton.Checked = false;
            }

            else
            {
                PersonRadioButton.Text = Name + " has no $ to bet";
                PersonLabel.Text = Name + " does not have anything to place a bet";
                PersonRadioButton.Enabled = false;
                PersonRadioButton.Checked = false;
            }
        }

        // reset to get ready for next bet.
        public void Clear()
        {
            PersonBet = null;
        }

        public bool PlaceBet(int Amount, int Racer)
        {
            // place a new bet and store it in person bet field.
            PersonBet = new Bet();

            if (Cash >= Amount)
            {
                PersonBet.Amount = Amount;
                PersonBet.Racer = Racer;
                PersonBet.Bettor = this;
                LabelsRadioBtnsUpdate();
                return true;
            }
            else if (Cash < Amount) return false;
            else return false;
        }

        //this method takes a winner.
        public void Collect(int Winner)
        {
            // then does a conditional test to see,
            // if Person placed a bet.
            // if person did place a bet we pay to a winner.
            if (PersonBet != null)
            {
                Cash += PersonBet.Pay(Winner);
            }
        }
    }
}