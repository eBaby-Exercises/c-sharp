using eBabyServices;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace eBaby.Tests
{
    static internal class FluentExtensions
    {
        public static AuctionAssertions Should(this Auction testSubject)
        {
            return new AuctionAssertions(testSubject);
        }

        public static PostOfficeAssertions Should(this PostOffice postOffice)
        {
            return new PostOfficeAssertions(postOffice);
        }
    }

    internal class PostOfficeAssertions : ReferenceTypeAssertions<PostOffice, PostOfficeAssertions>
    {
        public PostOfficeAssertions(PostOffice subject) : base(subject)
        {

        }

        protected override string Identifier => "PostOffice";

        public void HaveSeenMsg(Auction testSubject)
        {
            Subject.FindEmail(testSubject.Seller.UserEmail, string.Empty).Should()
                .Be($"<sendEMail address=\"{testSubject.Seller.UserEmail}\" >Hi</sendEmail>\n");
        }
    }
}