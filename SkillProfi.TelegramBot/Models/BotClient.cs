using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using Telegram.Bot.Polling;
using SkillProfi.TelegramBot.Data;
using Telegram.Bot.Types.Enums;

namespace SkillProfi.TelegramBot.Models
{
    public class BotClient
    {
        private int currentService = 0;
        private int currentProject = 0;
        private int currentBlog = 0;
        private bool isName = false;
        private bool isEmail = false;
        private bool isMessage = false;
        private Request request;
        private ITelegramBotClient _botClient;
        private DataApi _dataApi;
        private Message last;
        private InlineKeyboardMarkup menu = new(new[]
            {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Подать заявку", "/create_request")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Список проектов", "/projects"),
                            InlineKeyboardButton.WithCallbackData("Список услуг", "/services")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Список блогов", "/blogs"),
                            InlineKeyboardButton.WithCallbackData("Наши контакты", "/socials")
                        }
            });

        private InlineKeyboardMarkup prevNextProj = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_project"),
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_project")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        private InlineKeyboardMarkup nextProj = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_project")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        private InlineKeyboardMarkup prevProj = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_project")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });

        InlineKeyboardMarkup prevNextService = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_service"),
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_service")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        InlineKeyboardMarkup nextService = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_service")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        InlineKeyboardMarkup prevService = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_service")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });

        InlineKeyboardMarkup prevNextBlog = new(new[]
                {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_blog"),
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_blog")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        InlineKeyboardMarkup nextBlog = new(new[]
                {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Далее", "/next_blog")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });
        InlineKeyboardMarkup prevBlog = new(new[]
                {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Назад", "/prev_blog")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });

        public string Name { get; private set; }

        public BotClient(string token)
        {
            _botClient = new TelegramBotClient(token);
            _dataApi = new DataApi();
        }


        public void Start()
        {
            var cts = new CancellationTokenSource();
            var _cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };

            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                _cancellationToken
            );

            Name = _botClient.GetMeAsync().Result.FirstName;

            Console.WriteLine("Запущен бот " + Name);

            Console.ReadLine();
        }


        private async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update, CancellationToken cancellationToken)
        {
            ClearCallbackQueryMessage(cancellationToken);

            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                if (message.Text.ToLower() == "/start")
                {
                    SendMenu(message.Chat.Id);
                    await _botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id,
                        messageId: update.Message.MessageId,
                        cancellationToken: cancellationToken);
                }


                if (isName)
                {
                    if (message.Text.Split(' ').Length == 3)
                    {
                        request.Name = message.Text;
                        isName = false;
                        GetEmail(message.Chat.Id);
                    }
                    else
                    {
                        SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введены ФИО. Введите ФИО еще раз.");
                    }

                    ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (isEmail)
                {
                    if (message.Text.Contains("@"))
                    {
                        request.Email = message.Text;
                        isEmail = false;
                        GetMessage(message.Chat.Id);
                    }
                    else
                    {
                        SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введен Email. Введите Email еще раз.");
                    }

                    ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (isMessage)
                {
                    if (message.Text.Length >= 10)
                    {
                        isMessage = false;
                        request.Message = message.Text;
                        SendRequest(message.Chat.Id);
                    }
                    else
                    {
                        SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введено описание. Введите еще раз.");
                    }

                    ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (!message.Text.StartsWith("/"))
                {
                    SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введна команда, воспользуйтесь меню для удобства.");
                    if (update.Message != null)
                    {
                        await _botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id,
                            messageId: update.Message.MessageId,
                            cancellationToken: cancellationToken);
                    }
                }
            }

            if (update.Type == UpdateType.CallbackQuery)
            {
                HandleCallbackQuery(update.CallbackQuery);
            }
        }

        private void ClearCallbackQueryMessage(CancellationToken token)
        {
            if (last != null)
            {
                _botClient.DeleteMessageAsync(chatId: last.Chat.Id,
                        messageId: last.MessageId,
                        cancellationToken: token);
                last = null;
            }
        }

        private void ClearMessageInCreationRequest(long chatId, int messageId, CancellationToken token)
        {
            Task.Run(() =>
            {
                _botClient.DeleteMessageAsync(chatId,
                            messageId - 1,
                            token);
                _botClient.DeleteMessageAsync(chatId,
                                messageId,
                                token);
            });
        }

        private void SendRequest(long id)
        {
            _dataApi.SendRequest(request);
            string msg = "Ваша заявка успешно отправлена! " +
                "Для перехода в меню, нажмите кнопку Выход в меню.";
            SendMessageWithMenuBack(id, msg);
        }

        private void SendMessageWithMenuBack(long id, string msg)
        {
            InlineKeyboardMarkup exit = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });

            last = _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: exit).Result;
        }

        private async Task HandleErrorAsync(ITelegramBotClient botClient,
            Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }


        private void SendMenu(long id)
        {
            last = _botClient.SendTextMessageAsync(id, "Выберите один из пунктов",
                replyMarkup: menu).Result;
        }

        private void HandleCallbackQuery(CallbackQuery callbackQuery)
        {
            var socialLinks = Task.Run(_dataApi.GetSocialLinks).Result;
            var data = callbackQuery.Data;
            var services = Task.Run(_dataApi.GetServices).Result;
            var projects = Task.Run(_dataApi.GetProjects).Result;
            var blogs = Task.Run(_dataApi.GetBlogs).Result;

            switch (data)
            {
                case "/create_request":
                    CreateRequest(callbackQuery.Message.Chat.Id);
                    break;
                case "/services":
                    currentService = 0;
                    SendServices(services, callbackQuery.Message.Chat.Id);
                    break;
                case "/next_service":
                    SendServices(services, callbackQuery.Message.Chat.Id);
                    break;
                case "/prev_service":
                    currentService -= 2;
                    SendServices(services, callbackQuery.Message.Chat.Id);
                    break;
                case "/projects":
                    currentProject = 0;
                    SendProjects(projects, callbackQuery.Message.Chat.Id);
                    break;
                case "/next_project":
                    SendProjects(projects, callbackQuery.Message.Chat.Id);
                    break;
                case "/prev_project":
                    currentProject -= 2;
                    SendProjects(projects, callbackQuery.Message.Chat.Id);
                    break;
                case "/blogs":
                    currentBlog = 0;
                    SendBlogs(blogs, callbackQuery.Message.Chat.Id);
                    break;
                case "/next_blog":
                    SendBlogs(blogs, callbackQuery.Message.Chat.Id);
                    break;
                case "/prev_blog":
                    currentBlog -= 2;
                    SendBlogs(blogs, callbackQuery.Message.Chat.Id);
                    break;
                case "/socials":
                    SendSocialLinks(socialLinks, callbackQuery.Message.Chat.Id);
                    break;
                case "/exit":
                    SendMenu(callbackQuery.Message.Chat.Id);
                    break;
                default:
                    break;
            }
        }

        private void SendProjects(Project[] projects, long id)
        {
            var msg = $"{projects[currentProject].Title}\n\n{projects[currentProject].Description}";
            IReplyMarkup replyMarkup;

            if (currentProject == projects.Length - 1)
            {
                replyMarkup = prevProj;
                currentProject = 0;
            }
            else if (currentProject == 0)
            {
                replyMarkup = nextProj;
                currentProject++;
            }
            else
            {
                replyMarkup = prevNextProj;
                currentProject++;
            }

            last = _botClient.SendTextMessageAsync(id, msg,
                    replyMarkup: replyMarkup).Result;
        }

        private void SendServices(Service[] services, long id)
        {
            var msg = $"{services[currentService].Title}\n\n{services[currentService].Description}";
            IReplyMarkup replyMarkup;

            if (currentService == services.Length - 1)
            {
                replyMarkup = prevService;
                currentService = 0;
            }
            else if (currentService == 0)
            {
                replyMarkup = nextService;
                currentService++;
            }
            else
            {
                replyMarkup = prevNextService;
                currentService++;
            }

            last = _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: replyMarkup).Result;
        }

        private void SendBlogs(Blog[] blogs, long id)
        {

            var msg = $"{blogs[currentBlog].Title}\n\n{blogs[currentBlog].Description}";
            IReplyMarkup replyMarkup;

            if (currentBlog == blogs.Length - 1)
            {
                replyMarkup= prevBlog;
                currentBlog = 0;
            }
            else if (currentBlog == 0)
            {
                replyMarkup = nextBlog;
                currentBlog++;
            }
            else
            { 
                replyMarkup = prevNextBlog;
                currentBlog++;
            }

            last = _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: replyMarkup).Result;
        }

        private void SendSocialLinks(SocialLinks links, long id)
        {
            var msg = $"Youtube: {links.Youtube}\nTelegram: {links.Telegram}\nVK: {links.Vk}";
            last = _botClient.SendTextMessageAsync(id, msg,
                    disableWebPagePreview: true,
                    replyMarkup: menu).Result;
        }

        private void CreateRequest(long id)
        {
            request = new Request();
            GetFullName(id);
        }

        private void GetFullName(long id)
        {
            SendMessageWithMenuBack(id, "Введите свое ФИО.");
            isName = true;
        }

        private void GetEmail(long id)
        {
            SendMessageWithMenuBack(id, "Введите свой Email.");
            isEmail = true;
        }

        private void GetMessage(long id)
        {
            SendMessageWithMenuBack(id, "Введите описание проблемы.");
            isMessage = true;
        }
    }
}
