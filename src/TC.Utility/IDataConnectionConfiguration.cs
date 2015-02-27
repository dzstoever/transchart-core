namespace TC.Tools.DbUtility
{
	public interface IDataConnectionConfiguration
	{
		string GetSelectedSource();
		void SaveSelectedSource(string provider);

		string GetSelectedProvider();
		void SaveSelectedProvider(string provider);
	}
}
