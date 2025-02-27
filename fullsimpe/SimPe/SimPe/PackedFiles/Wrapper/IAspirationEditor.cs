namespace SimPe.PackedFiles.Wrapper
{
	public interface IAspirationEditor
	{
		Data.MetaData.AspirationTypes[] LoadAspirations(SDesc sim);
		void StoreAspirations(Data.MetaData.AspirationTypes[] asps, SDesc sim);
	}
}
