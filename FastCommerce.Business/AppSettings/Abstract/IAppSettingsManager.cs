using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Entities.Entities;

namespace FastCommerce.Business.AppSettings.Abstract
{
    public interface IAppSettingsManager
    {
        public Task<bool> AddSliderImage(SliderImage sliderImage);
        public Task<List<SliderImage>> GetSliderImages();
        public Task<bool> UpdateSliderImage(SliderImage sliderImage);
    }
}
