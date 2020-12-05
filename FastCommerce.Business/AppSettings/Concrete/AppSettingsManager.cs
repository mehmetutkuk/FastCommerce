using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastCommerce.Business.AppSettings.Abstract;
using FastCommerce.DAL;
using FastCommerce.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastCommerce.Business.AppSettings.Concrete
{
    public class AppSettingsManager: IAppSettingsManager
    {
        private readonly dbContext _context;

        public AppSettingsManager(dbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSliderImage(SliderImage sliderImage)
        {
            await _context.SliderImages.AddAsync(sliderImage);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteSliderImage(SliderImage sliderImage)
        {
            var sliderImageEntity = _context.SliderImages.Single(item => item.SliderImageId == sliderImage.SliderImageId);
            _context.SliderImages.Remove(sliderImageEntity);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        public async Task<List<SliderImage>> GetSliderImages() => await _context.SliderImages.ToListAsync();

        public async Task<bool> UpdateSliderImage(SliderImage sliderImage)
        {
            var sliderImageEntity = _context.SliderImages.Single(item => item.SliderImageId == sliderImage.SliderImageId);
            sliderImageEntity = sliderImage;
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
