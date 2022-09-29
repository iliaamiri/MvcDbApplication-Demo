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

    [Authorize]
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Add(Member member)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            await _membersRepository.AddMember(member);
            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }

    [Authorize]
    [HttpGet]
    [Route("[controller]/[action]/{memberId}")]
    public async Task<IActionResult> Delete(int memberId)
    {
        try
        {
            await _membersRepository.DeleteMemberById(memberId);
            return RedirectToAction("Index");
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }

    [Authorize]
    [HttpGet]
    [Route("[controller]/[action]/{memberId}")]
    public async Task<IActionResult> Edit(int? memberId)
    {
        try
        {
            if (memberId == null || memberId == 0)
                return NotFound();

            var memberToEdit = await _membersRepository.GetMemberById((int)memberId);
            
            if (memberToEdit == null)
                return NotFound();

            return View(memberToEdit);
        }
        catch (Exception exception)
        {
            return BadRequest();
        }
    }
    
    [Authorize]
    [HttpPost]
    [Route("[controller]/[action]/{memberId}")]
    public async Task<IActionResult> Edit(Member member)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }

            await _membersRepository.UpdateMember(member);

            return RedirectToAction("Index");
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
