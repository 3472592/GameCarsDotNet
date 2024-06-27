
namespace LabProj
{
    /// <summary>
    /// Class Bet, is in charge for keeping bet amount, racer "ID",
    /// Person's object that will be used in Bettor role.
    /// </summary>
    public class Bet
    {
        public int Amount; // amount of betted cash.
        public int Racer; // # of Racer bet is on.
        public Person Bettor = new(); // Person that placed a bet.

        // this method is used for updating betting parlor labels.
        public string BettingParlorUpdateLabel()
        {
            // inside this method, conditional statements, are using
            // Person class properties. Name & Cash.
            if (Amount >= 5 && Amount < Bettor.Cash) return Bettor.Name + " bets $" + Amount + " on Racer #" + Racer;
            
            else if (Amount == 0) return Bettor.Name + " has not placed a bet";

            else if (Amount == Bettor.Cash) return Bettor.Name + " bet all cash on Racer #" + Racer;
            
            else return "Error...";
        }

        //this method pays winner after making a bet & race.
        public int Pay(int Winner)
        {
            // if racer won, return amount bet.
            // if lost take away bet amount.
            return (Winner == Racer) ? Amount * 2 : -1 * Amount;
        }
    }
}