using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcDbApplication.Data.Baraga;
using MvcDbApplication.Models;
using MvcDbApplication.Services;

namespace MvcDbApplication.Controllers;

public class MembersController : Controller
{
    private readonly ILogger<MembersController> _logger;
    private readonly IMembersRepositry _membersRepository;

    public MembersController(ILogger<MembersController> logger, IMembersRepositry membersRepository)
    {
        _logger = logger;
        _membersRepository = membersRepository;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        try
        {
            var members = await _membersRepository.GetMembers();
            ViewBag.members = members;
            return View();
        }
        catch (Exception exception)
        {
            return Error();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return await Task.Run(() =>
        {
            try
            {
                return View();
            }
            catch (Exception exception)
            {
                return Error();
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(Member member)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            var addedMember = await _membersRepository.AddMember(member);
            return View(addedMember);
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
