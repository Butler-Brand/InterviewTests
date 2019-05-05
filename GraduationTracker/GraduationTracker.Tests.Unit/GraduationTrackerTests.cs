using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {

        List<Tuple<bool, STANDING>> graduated;

        //The unit test is essentially copied right out of "Repository.cs",
        //so why don't we just use that data? Then write up that guy.

        //we'll use the 4-student standby as the base for all our tests as it is
        //in the repository class.
        private List<Tuple<bool, STANDING>> TestSetup()
        {
            var tracker = new GraduationTracker();
            Diploma testDiploma = Repository.GetDiploma(1); //Get the diploma of ID 1
            Student[] testStudents = new Student[4];

            //initialize students array -- use all 4 in repository
            for (int i = 0; i < 4; i++)
            {
                testStudents[i] = Repository.GetStudent(i + 1);
            }

            var graduated = new List<Tuple<bool, STANDING>>();

            foreach (var student in testStudents)
            {
                graduated.Add(tracker.HasGraduated(testDiploma, student));
            }
            return graduated;
        }

        //We'll divide this up into 3 different tests. Ideally we might have only
        //1 Assert per test method, but for this I'll divide it up by testing the Tuple List
        //existence & size, then values of the first item, and finally values of the second item
        [TestMethod]
        public void TestHasCredits() //Original test method
        {
            List<Tuple<bool, STANDING>> graduated = TestSetup();

            //This should only be checking if there are any entries in Graduated.
            //a valid test, but others would be more appropriate
            Assert.IsNotNull(graduated); //This should not be null

            Assert.IsTrue(graduated.Any()); //This is the original test from the supplied code

            Assert.IsTrue(graduated.Count == 4); //Size of the list should be 4

        }

        [TestMethod]
        public void TestGraduates() //Test the graduate numbers
        {
            List<Tuple<bool, STANDING>> graduated = TestSetup();

            int gradNum = 0, failNum = 0;

            foreach(var graduate in graduated)
            {
                if (graduate.Item1)
                    gradNum++;

                else
                    failNum++;
            }

            Assert.IsTrue(gradNum == 3); //Should be 3 graduates

            Assert.IsTrue(failNum == 1); //Should be 1 failure

        }


        [TestMethod]
        public void TestStandings() //Test the Graduate Standings.
        {
            List<Tuple<bool, STANDING>> graduated = TestSetup();

            int sumaCumLaudeNum = 0, magnaCumLaudeNum = 0, averageNum = 0, remedialNum = 0, noStanding = 0;

            foreach (var graduate in graduated)
            {
                switch (graduate.Item2)
                {
                    case STANDING.SumaCumLaude:
                        sumaCumLaudeNum++;
                        break;

                    case STANDING.MagnaCumLaude:
                        magnaCumLaudeNum++;
                        break;

                    case STANDING.Average:
                        averageNum++;
                        break;

                    case STANDING.Remedial:
                        remedialNum++;
                        break;

                    case STANDING.None:
                        noStanding++;
                        break;

                    default:
                        continue;
                }

            }

            Assert.IsTrue(sumaCumLaudeNum == 0); //Should be 0 Suma Cum Laudes

            Assert.IsTrue(magnaCumLaudeNum == 2); //Should be 2 Magna Cum Laudes (95 & 80)

            Assert.IsTrue(averageNum == 1); //Should be 1 Average (50)

            Assert.IsTrue(remedialNum == 1); //Should be 1 Remedial (40)

            Assert.IsTrue(noStanding == 0); //Should be 0 'None' Standings

        }

    }
}
