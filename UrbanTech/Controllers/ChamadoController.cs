using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrbanTech.Data.Contexts;
using UrbanTech.Models;
using UrbanTech.ViewModel;

namespace UrbanTech.Controllers
{
    public class ChamadoController : Controller
    {

        private List<ChamadoModel> chamados;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ChamadoController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_context.Chamados);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ChamadoCreateViewModel();

            int pageSize = 10;
            int pageNumber = 1; 
            var ChamadosPaginados = _context.Chamados
                .OrderBy(c => c.Nome)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ChamadoCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var chamadoModel = _mapper.Map<ChamadoModel>(viewModel);

                _context.Chamados.Add(chamadoModel);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = "Chamado registrado com sucesso!";
                return RedirectToAction(nameof(Index));
            } else
            {
                return View(viewModel);
            }

            
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var chamadoEditado = _context.Chamados.Find(id);

            return View(chamadoEditado);
        }

        [HttpPost]
        public IActionResult Edit(ChamadoModel chamadoModel)
        {
            _context.Chamados.Update(chamadoModel);
            _context.SaveChanges();
            TempData["mensagemSucesso"] = "Chamado alterado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var chamadoConsultado = _context.Chamados.FirstOrDefault(c => c.Id == id);

            return View(chamadoConsultado);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var chamado = _context.Chamados.Find(id);
            if (chamado != null)
            {
                _context.Chamados.Remove(chamado);
                _context.SaveChanges();
                TempData["mensagemSucesso"] = $"O chamado de {chamado.Nome} foi removido com sucesso";
            }
            else
            {
                TempData["mensagemSucesso"] = "Algo deu errado! tente novamente mais tarde.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

