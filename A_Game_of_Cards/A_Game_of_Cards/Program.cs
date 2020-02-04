using System;
using System.Linq;

namespace A_Game_of_Cards
{
    public class Player 
    {
        public Player(string name)
        {
            _name = name;
        }
        
        private string _name;
        Cards[] _Hand = new Cards[0];
        

        public void GiveCard(Cards[] cards)
        {
            var z = new Cards[_Hand.Length + cards.Length];
            _Hand.CopyTo(z, 0);
            cards.CopyTo(z, _Hand.Length);
            Array.Resize<Cards>(ref _Hand,z.Length);
            Array.Copy(z, 0, _Hand, 0, z.Length);
        }
        public void GetHand()
        {
            for(int i = 0; i <_Hand.Length; i++)
            {
                Console.WriteLine("Player:" + _name);
                _Hand[i].GetCard();
            }
        }
        public int Total()
        {
            int total = 0;
            for (int i = 0; i < _Hand.Length; i++)
            {
                total += _Hand[i].getVal();
            }
            return total;
        }
        public string GetName()
        {
            return _name;
        }
    }

    public class Game
    {
        public Player[] _players;
        private int _playersAmount;
        public Game(int players)
        {
            _playersAmount = players;
            _players = new Player[players];
            for(int i = 0; i < _playersAmount; i++)
            {  
                Console.WriteLine("What is the name of player #" + i ); ;
                string name = Console.ReadLine();
                _players[i] = new Player(name);
            }

        }
        Deck _deck = new Deck();
       
        public void Dealcard(int number)
        {
            int i = 0;
            while(i!= _playersAmount)
            { 
                _players[i].GiveCard(_deck.DealCards(number));
                i++;
            }
        }
        public void playerCards()
        {
            int i = 0;
            while (i != _playersAmount)
            {
                _players[i].GetHand();
                i++;
            }
        }
        public void Winner()
        {
            int i = 0;
            int[] totals = new int[_playersAmount];
            while (i != _playersAmount)
            {
                totals[i] = _players[i].Total();
                //Total().IndexOf(maxValue);
                i++;
            }
            int maxValue = totals.Max();
            int maxIndex = totals.ToList().IndexOf(maxValue);
            Console.WriteLine("Winner is Player Name: " + _players[maxIndex].GetName() + " With " + _players[maxIndex].Total() + " Points ");
        }
    }
    public class Deck
    {
        public Cards[] DealCards(int numberOfCards)
        {
            Cards[] cardArray = new Cards[numberOfCards];
            for (int i = 0; i < numberOfCards; i++)
            {
                cardArray[i] = new Cards();
            }
            return cardArray;
        }
    }
    public class Cards : Deck
    {
        private string _colour;
        private int _number;
        private int _value;
        public Cards()
        {
            CardGen();
            switch (_colour)
            {
                case "Red":
                    _value = 1 * _number;
                    break;
                case "Blue":
                    _value = 2 * _number;
                    break;
                case "Green":
                    _value = 3 * _number;
                    break;
                case "Yellow":
                    _value = 4 * _number;
                    break;
                default:
                    Console.WriteLine("Error, Colour unknown");
                    break;
            }
        }

        public void GetCard()
        {
            Console.WriteLine("Colour: " + _colour + " Number: " + _number + " Total: " + _value);
        }
        public int getVal()
        {
            return _value;
        }
        private void CardGen()
        {
            var rand = new Random();
            int gen = rand.Next(1, 4);
            _number = rand.Next(1, 8);
            switch (gen)
            {
                case 1:
                    _colour = "Red";
                    break;
                case 2:
                    _colour = "Blue";
                    break;
                case 3:
                    _colour = "Green";
                    break;
                case 4:
                    _colour = "Yellow";
                    break;
                default:
                    Console.WriteLine("Error Colour gen");
                    break;
            }

        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the card game, how many players, are playing?");
            int players = Convert.ToInt32(Console.ReadLine());
            var test = new Game(players);
            Console.WriteLine("How many cards are we playing with?");
            int cards = Convert.ToInt32(Console.ReadLine());
            test.Dealcard(cards);
            test.playerCards();
            test.Winner();
        }
    }
}

