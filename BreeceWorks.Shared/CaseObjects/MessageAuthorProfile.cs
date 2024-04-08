namespace BreeceWorks.Shared.CaseObjects
{
    public partial class MessageAuthorProfile
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }

            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                }
            }
        }

        public string LastName
        {
            get { return _lastName; }

            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                }
            }
        }

    }
}
