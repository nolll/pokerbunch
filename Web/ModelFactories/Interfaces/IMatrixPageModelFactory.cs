using Core.Classes;
using Web.Models;
using app;

namespace Web.ModelFactories{
    public interface IMatrixPageModelFactory {

		MatrixPageModel Create(Homegame homegame, User user, int? year);

	}

}