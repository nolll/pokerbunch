﻿using Core.Entities;
using Core.UseCases.EventList;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Core.UseCases
{
    class EventListTests : TestBase
    {
        [Test]
        public void EventList_ReturnsAllEvents()
        {
            var result = Sut.Execute(CreateInput());

            Assert.AreEqual(2, result.Events.Count);
        }

        [Test]
        public void EventList_EachItem_NameIsSet()
        {
            var result = Sut.Execute(CreateInput());

            Assert.AreEqual(TestData.EventNameB, result.Events[0].Name);
            Assert.AreEqual(TestData.EventNameA, result.Events[1].Name);
        }

        [Test]
        public void EventList_EachItem_StartDateIsSet()
        {
            var result = Sut.Execute(CreateInput());

            Assert.AreEqual(new Date(2002, 2, 2), result.Events[0].StartDate);
            Assert.AreEqual(new Date(2001, 1, 1), result.Events[1].StartDate);
        }

        [Test]
        public void EventList_EachItem_EndDateIsSet()
        {
            var result = Sut.Execute(CreateInput());

            Assert.AreEqual(new Date(2002, 2, 3), result.Events[0].EndDate);
            Assert.AreEqual(new Date(2001, 1, 2), result.Events[1].EndDate);
        }

        [Test]
        public void EventList_EachItem_UrlIsSet()
        {
            var result = Sut.Execute(CreateInput());

            Assert.AreEqual("/bunch-a/event/details/2", result.Events[0].EventDetailsUrl.Relative);
            Assert.AreEqual("/bunch-a/event/details/1", result.Events[1].EventDetailsUrl.Relative);
        }

        private EventListInteractor Sut
        {
            get
            {
                return new EventListInteractor(
                    Repos.Bunch,
                    Repos.Event);
            }
        }
        
        private EventListInput CreateInput()
        {
            return new EventListInput(TestData.SlugA);
        }
    }
}
