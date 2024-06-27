
namespace LabProj
{
    public partial class Form1 : Form
    { 
        private readonly Person[] Bettors; // Bettors array object that comes from Person class.
        private readonly RacersMotion[] Racers; // Racers array object that comes from RacersMotion class.
        private Person CurrentBettor; // new Person obj named Current Bettor.

        public Form1()
        {
            InitializeComponent();
            // to make game interesting will use random.
            Random rand = new();
            Bettors = new Person[3]; // Bettors array of Person type includes 3 people with their unique info.
            Racers = new RacersMotion[3]; // Racers array of type Racers motion includes 3 racers.
            int cash = 50;
            Bettors = new[] // initializing new array with all available Bettors.
            {
                new Person("Yarik", cash, Name1RadioBtn, Name1Label),
                new Person("Vova", cash, Name2RadioBtn, Name2Label),
                new Person("Vovchik", cash, Name3RadioBtn, Name3Label)
            };
                // All the bettors need to be connected to Label Update functionality.
            Bettors[0].LabelsRadioBtnsUpdate();
            Bettors[1].LabelsRadioBtnsUpdate();
            Bettors[2].LabelsRadioBtnsUpdate();
                // Start Position is location of racer picture box 1 horizontal position.
            int StartPosition = pictureBox1.Location.X;
                // distance width is a picture box named TrackLen.
            int distance = TrackLen.Width;
            for (int i = 0; i < Racers.Length; i++) // loop through racers array.
            {   // take racers array with its index.
                Racers[i] = new RacersMotion
                {
                    Rand = rand, // make new var = to rand.
                    RacetrackLength = distance // make new var = to distance
                };
                // racers location is racers start pos is Starting Position.
                Racers[i].Location = Racers[i].StartingPosition = StartPosition;
            }
            //telling C# what role each Racer from array will play.
            Racers[0].PersonPicBox = pictureBox1;
            Racers[1].PersonPicBox = pictureBox2;
            Racers[2].PersonPicBox = pictureBox3;
            CurrentBettor = Bettors[0]; // Current bettor is the 1st bettor when starting up the game.
        }
        // keep radio buttons enabled, so user can use a bettor.
        public void RadioBtnStatusTrue()
        {
            Bettors[0].PersonRadioButton.Enabled = true;
            Bettors[1].PersonRadioButton.Enabled = true;
            Bettors[2].PersonRadioButton.Enabled = true;
        }
        // keep radio buttons disabled, so user can't a bettor.
        public void RadioBtnStatusFalse()
        {
            if (CurrentBettor.Cash == 0)
            {
                Bettors[0].PersonRadioButton.Enabled = false;
                Bettors[1].PersonRadioButton.Enabled = false;
                Bettors[2].PersonRadioButton.Enabled = false;
            }
        }

        public void RadioButtonsFalse() 
        {
            Bettors[0].PersonRadioButton.Enabled = false;
            Bettors[1].PersonRadioButton.Enabled = false;
            Bettors[2].PersonRadioButton.Enabled = false;
        }
        public void BettorMinAndMax()
        {
            BetAmount.Minimum = 5;
            BetAmount.Maximum = CurrentBettor.Cash;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            minBet.Text = "Min Bet: " + BetAmount.Minimum;
            if (CurrentBettor.Cash > 4)
            {
                BettorMinAndMax();
                RadioBtnStatusTrue(); // keep radio btns enabled.
            }
            else RadioBtnStatusFalse(); // keep radio btns disabled.
        }
        // as soon as got a winner, use this method.
        public void WinnerRules()
        {
            Race.Enabled = false;
        }

        public void Replay()
        {
            WinnerRules();
            if (Race.Text == "Racing...")
            {
                Race.Text = "Race";
                RadioBtnStatusTrue();
                BetBtnStatus();

                BetAmount.Enabled = true;
                RacerNum.Enabled = true;
                Race.Enabled = true;

                //pic box locations Y & X axis's.
                pictureBox1.Location = new Point(15, 121);
                pictureBox2.Location = new Point(15, 297);
                pictureBox3.Location = new Point(15, 476);
            }
        }
        private void Race_Click(object sender, EventArgs e)
        {
            RadioBtnStatusFalse(); // when racing radio buttons are unavailable.
            Race.Enabled = false;
            BetAmount.Enabled = false;
            RacerNum.Enabled = false;
            BetBtn.Enabled = false;
            RadioButtonsFalse();
            var winner = 0; // tells winner # after race is over.
            var num_winners = 0; // keeps track if there is a winner.
            while (num_winners == 0) // while there no winner yet.
            {
                Race.Text = "Racing...";
                // looping through Racers array length.
                for (int i = 0; i < Racers.Length; i++)
                {
                    // when all racers move to reach finish.
                    if (Racers[i].MoveToReachFinish())
                    {
                        num_winners++; // raise to one so we have a winner.
                        // do i + 1, so user won't see "Winner #0 won",
                        // and will see "Winner #1 won", etc...
                        winner = i + 1;
                    }
                }
                //shows our racers whole time while racing.
                Application.DoEvents();
                // could make racers in slow motion by adding greater #,
                // could make racers in fast motion by adding lesser #.
                Thread.Sleep(3);
            }
            if (num_winners == 1) // if we have someone who reached finish 1st, thats our winner.
            {
                MessageBox.Show("Racer #" + winner + " wins!");
                Replay();
            }

            else if (num_winners >= 2) 
            {
                MessageBox.Show("We have " + num_winners + " winners");
                Replay();
            }
            //looping through bettors array length.
            for (int i = 0; i < Bettors.Length; i++)
            {
                /*
                 * Pass the winner into Collect() from Person class to verify if Winner had a bet on it.
                 * If conditional test is true then we go to Bet class to it's Pay().
                 * And that is where decision of paying out to bettors will happen, or money get taken away.
                 */
                Bettors[i].Collect(winner);
                Bettors[i].Clear(); // clear out person's bet automatically so user has easier life.
                Bettors[i].LabelsRadioBtnsUpdate(); // update radio buttons so bettors have new cash values.
            }
            // what is being placed by user is the value
            // and bet that being placed should be minimum or more only.
            BetAmount.Value = BetAmount.Minimum;
            RacerNum.Value = RacerNum.Minimum;
        }
        private void BetBtn_Click(object sender, EventArgs e)
        {
            // place bet amount value as an argument, same with racer num value. So current bettor can place a bet.
            CurrentBettor.PlaceBet((int)BetAmount.Value, (int)RacerNum.Value);
            CurrentBettor.LabelsRadioBtnsUpdate(); //update values of current bettor after placing a bet.
        }
        private void SetBettor(int index)
        {
            CurrentBettor = Bettors[index]; // current bettor could be anyone inside Bettors array.
            nameLabel.Text = CurrentBettor.Name; // show who's radio button was selected.
            BetAmount.Maximum = Bettors[index].Cash;

            if (CurrentBettor.PersonBet != null) // if current bettor placed a bet.
            {
                BetAmount.Value = CurrentBettor.PersonBet.Amount;
                RacerNum.Value = CurrentBettor.PersonBet.Racer;
            }
        }
        private void NameRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (Name1RadioBtn.Checked)
            {
                SetBettor(0); // set bettor 1.
                BetBtnStatus();
            }
            else if (Name2RadioBtn.Checked)
            {
                SetBettor(1); // set bettor 2.
                BetBtnStatus();
            }
            else if (Name3RadioBtn.Checked)
            {
                SetBettor(2); // set bettor 3.
                BetBtnStatus();
            }
            else if (!Name1RadioBtn.Checked)
            {
                SetBettor(0);
                BetBtnStatus();
            }
            else if (!Name2RadioBtn.Checked)
            {
                SetBettor(1);
                BetBtnStatus();
            }
            else if (!Name3RadioBtn.Checked)
            {
                SetBettor(2);
                BetBtnStatus();
            }
        }
        private void BetBtnStatus()
        {
            if (CurrentBettor.Cash <= 4)
            {
                BetBtn.Enabled = false;
                BetAmount.Enabled = false;
            }
            else if (CurrentBettor.Cash == 0)
            {
                BetBtn.Enabled = false;
                BetAmount.Enabled = false;
            }
            else if (CurrentBettor.Cash >= 5)
            {
                BetBtn.Enabled = true;
                BetAmount.Enabled = true;
            }
        }
        private void BetAmount_ValueChanged(object sender, EventArgs e)
        {
            BettorMinAndMax();
        }
    }
}