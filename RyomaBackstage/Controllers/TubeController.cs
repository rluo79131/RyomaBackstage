using RyomaBackstage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RyomaBackstage.Controllers
{
    public class TubeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crop(TubeRequest request)
        {
            var response = new List<TubeCropResult>();
            var totalQuantity = request.TubeSettings.Sum(e => e.Quantity);
            var serialNumber = 0;

            while (totalQuantity > 0)
            {
                serialNumber = serialNumber + 1;

                var tubeLength = request.TubeLength;
                var tubeLists = new List<string>();

                foreach (var tubeSetting in request.TubeSettings)
                {
                    //若管料已裁剪完則跳過
                    if (tubeSetting.Quantity == 0)
                    {
                        continue;
                    }

                    //計算_管料最多裁剪數量
                    var quantity = -1;
                    if (tubeSetting.Length * tubeSetting.Quantity < tubeLength)
                    {
                        quantity = tubeSetting.Quantity;
                    }
                    else
                    {
                        quantity = tubeLength / tubeSetting.Length;
                    }

                    //若裁剪數量為0則跳過
                    if (quantity == 0)
                    {
                        continue;
                    }

                    //計算_裁剪長度
                    var length = quantity * tubeSetting.Length;
                    tubeLength = tubeLength - length;

                    //填寫_管料裁剪結果資料
                    tubeLists.Add($"{tubeSetting.Length}公尺 * {quantity}根");

                    //更新_管料資料
                    tubeSetting.Quantity = tubeSetting.Quantity - quantity;
                    totalQuantity = totalQuantity - quantity;
                }

                response.Add(new TubeCropResult
                {
                    SerialNumber = serialNumber,
                    TubeList = string.Join("<br />", tubeLists),
                    RemainingLength = tubeLength,
                });
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}