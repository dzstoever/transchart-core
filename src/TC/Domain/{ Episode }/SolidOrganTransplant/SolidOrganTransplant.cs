namespace TC.Domain.SolidOrganTransplant
{
    public static class SolidOrganTransplant
    {

        public enum Types
        {
            D,
            R
        }

        public enum Phases
        {
            Ref,
            Eval,
            MT, WL,
            DT, TX

        }

        public enum Discriminators
        {
                         
            H,          //Heart
            L,          //Liver    
            K, LK, RK,  //Kidney
            E, LL, RL,  //Lung
            P,          //Pancreas
            G           //Small Intestine
        }


        public static string GetName(this Types code)
        {
            switch (code)
            {
                case Types.D: return "Donor";
                case Types.R: return "Recipient";
                default:
                    return "Unknown code";
            }
        }

        public static string GetName(this Phases code)
        {
            switch (code)
            {
                case Phases.Ref: return "Referral";    // Both
                case Phases.Eval: return "Evaluation";  // Both
                case Phases.MT: return "Matching";    // Donor
                case Phases.WL: return "WaitListed";  // Recipient
                case Phases.DT: return "Donated";     // Donor
                case Phases.TX: return "Transplanted";// Recipient
                default:
                    return "Unknown code";
            }
        }

        public static string GetName(this Discriminators code)
        {
            switch (code)
            {
                case Discriminators.H: return "Heart";
                case Discriminators.L: return "Liver";
                case Discriminators.K: return "Kidney";
                case Discriminators.LK: return "LeftKidney";
                case Discriminators.RK: return "RightKidney";
                case Discriminators.E: return "Lung";                
                case Discriminators.LL: return "LeftLung";
                case Discriminators.RL: return "RightLung";
                case Discriminators.P: return "Pancrease";
                case Discriminators.G: return "SmallIntestine";
                default:
                    return "Unknown code";
            }
        }

        /// <summary>
        /// Return a string represention of status stored as a bool?
        /// 1="Active" 0="InActive" null="OnHold"
        /// </summary>
        public static string ToStatus(this bool? status)
        {
            if (status == null) return "OnHold";
            return status.Value ? "Active" : "Inactive";
        }

        /// <summary>
        /// Return a string represention of laterality stored as a bool?
        /// 1="Left" 0="Right" null=""(N/A)
        /// </summary>
        public static string ToLaterality(this bool? laterality)
        {
            if (laterality == null) return "";
            return laterality.Value ? "Left" : "Right";
        }

    }
}