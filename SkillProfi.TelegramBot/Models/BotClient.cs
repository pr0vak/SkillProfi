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
        /// <summary>
        /// Текущая услуга, которую просматривает пользователь.
        /// </summary>
        private int currentService = 0;

        /// <summary>
        /// Текущй проект, который просматривает пользователь.
        /// </summary>
        private int currentProject = 0;

        /// <summary>
        /// Текущий блог, который просматривает пользователь.
        /// </summary>
        private int currentBlog = 0;

        /// <summary>
        /// Пользователь вводит имя?
        /// </summary>
        private bool isName = false;

        /// <summary>
        /// Пользователь вводит почту?
        /// </summary>
        private bool isEmail = false;

        /// <summary>
        /// Пользователь вводит описание проблемы?
        /// </summary>
        private bool isMessage = false;

        /// <summary>
        /// Заявка, которую заполняет пользователь.
        /// </summary>
        private Request request;

        /// <summary>
        /// Телеграм бот.
        /// </summary>
        private ITelegramBotClient _botClient;

        /// <summary>
        /// Провайдер, связывающий Телеграм бота и сервер Web API.
        /// </summary>
        private DataApi _dataApi;

        /// <summary>
        /// Последнее полученное сообщение.
        /// </summary>
        private Message last;

        /// <summary>
        /// Имя Телеграм бота.
        /// </summary>
        public string Name { get; private set; }

        // Кнопки
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

        private InlineKeyboardMarkup prevNextService = new(new[]
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
        private InlineKeyboardMarkup nextService = new(new[]
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
        private InlineKeyboardMarkup prevService = new(new[]
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

        private InlineKeyboardMarkup prevNextBlog = new(new[]
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
        private InlineKeyboardMarkup nextBlog = new(new[]
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
        private InlineKeyboardMarkup prevBlog = new(new[]
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


        public BotClient(string token)
        {
            Initialize(token);
        }

        private void Initialize(string token)
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


        /// <summary>
        /// Обработчик сообщений пользователя.
        /// </summary>
        private async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update, CancellationToken cancellationToken)
        {
            // очищаем предыдущий Callback от Телеграм бота.
            await ClearCallbackQueryMessage(cancellationToken);

            // Если тип сообщения - текст...
            if (update.Type == UpdateType.Message)
            {
                var message = update.Message;

                // Если пользователь прислал '/start'
                if (message.Text.ToLower() == "/start")
                {
                    // Отправляем меню и удаляем сообщение пользователя.
                    await SendMenu(message.Chat.Id);
                    try
                    {
                        await _botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id,
                            messageId: update.Message.MessageId,
                            cancellationToken: cancellationToken);
                    }
                    catch 
                    {
                        Console.WriteLine("Была произведена попытка удаления предыдущих сообщений перед" +
                            " командой /start");
                    }
                }


                // Если пользователь вводит имя...
                if (isName)
                {
                    // Проверяем, что имя пользователя состоит из 3 частей (ФИО)
                    if (message.Text.Split(' ').Length == 3)
                    {
                        // Сохраняем в заявке имя пользователя и переходим к почте
                        request.Name = message.Text;
                        isName = false;
                        GetEmail(message.Chat.Id);
                    }
                    else
                    {
                        // иначе отправляем сообщение о некорректности заполнении имени
                        await SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введены ФИО. Введите ФИО еще раз.");
                    }

                    // очищаем предыдущие сообщения
                    await ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (isEmail) // если пользователь вводит почту
                {
                    // проверям, содержит ли почта значок '@'...
                    if (message.Text.Contains("@"))
                    {
                        // сохраняем информацию о почте в заявке
                        request.Email = message.Text;
                        isEmail = false;
                        GetMessage(message.Chat.Id); // переходит к описанию проблемы
                    }
                    else
                    {
                        // иначе отправляем сообщение о некорректности заполнении почты
                        await SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введен Email. Введите Email еще раз.");
                    }

                    // очищаем предыдущие сообщения
                    await ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (isMessage) // Если пользователь вводит описание проблемы
                {
                    // Проверяем, что сообщение не короткое...
                    if (message.Text.Length >= 10)
                    {
                        // сохраняем информацию о проблеме в заявку
                        isMessage = false;
                        request.Message = message.Text;
                        await SendRequest(message.Chat.Id);  // отправляем заявку на сервер
                    }
                    else
                    {
                        // иначе отправляем пользователю об ошибке заполнения
                        await SendMessageWithMenuBack(message.Chat.Id,
                            "Описание слишком короткое. Введите еще раз.");
                    }

                    // очищаем предыдущие сообщения
                    await ClearMessageInCreationRequest(update.Message.Chat.Id, update.Message.MessageId,
                            cancellationToken);
                }
                else if (!message.Text.StartsWith("/")) // если пользователь вводит текст,
                                                        // не начинающийся на '/'
                {
                    await SendMessageWithMenuBack(message.Chat.Id,
                            "Некорректно введна команда, воспользуйтесь меню для удобства.");
                    if (update.Message != null)
                    {
                        await _botClient.DeleteMessageAsync(chatId: update.Message.Chat.Id,
                            messageId: update.Message.MessageId,
                            cancellationToken: cancellationToken);
                    }
                }
            }

            // Если тип сообщение - кнопка, нажатая пользователем в меню
            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(update.CallbackQuery);
            }
        }

        /// <summary>
        /// Очистить последний Callback от Телеграм бота.
        /// </summary>
        private async Task ClearCallbackQueryMessage(CancellationToken token)
        {
            if (last != null)
            {
                await _botClient.DeleteMessageAsync(chatId: last.Chat.Id,
                        messageId: last.MessageId,
                        cancellationToken: token);
                last = null;
            }
        }

        /// <summary>
        /// Очистка сообщений в чате во время создания заявки.
        /// </summary>
        private async Task ClearMessageInCreationRequest(long chatId, 
            int messageId, CancellationToken token)
        {
            //await _botClient.DeleteMessageAsync(chatId,
            //                messageId - 1,
            //                token);
            await _botClient.DeleteMessageAsync(chatId,
                                messageId,
                                token);
        }

        /// <summary>
        /// Отправка заявки.
        /// </summary>
        /// <param name="id">Id чата.</param>
        private async Task SendRequest(long id)
        {
            await _dataApi.SendRequest(request);
            string msg = "Ваша заявка успешно отправлена! " +
                "Для перехода в меню, нажмите кнопку Выход в меню.";
            await SendMessageWithMenuBack(id, msg);
        }

        /// <summary>
        /// Отправить сообщение с кнопкой 'Выход в меню'
        /// </summary>
        /// <param name="id">Id чата.</param>
        /// <param name="msg">Сообщение.</param>
        private async Task SendMessageWithMenuBack(long id, string msg)
        {
            isName = false;
            isEmail = false; 
            isMessage = false;
            InlineKeyboardMarkup exit = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Выход в меню", "/exit")
                        }
            });

            last = await _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: exit);
        }

        /// <summary>
        /// Обработчик ошибок.
        /// </summary>
        private async Task HandleErrorAsync(ITelegramBotClient botClient,
            Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        /// <summary>
        /// Отправить меню.
        /// </summary>
        /// <param name="id">Id чата.</param>
        private async Task SendMenu(long id)
        {
            last = await _botClient.SendTextMessageAsync(id, "Выберите один из пунктов",
                replyMarkup: menu);
        }

        /// <summary>
        /// Обаботка Callback меню.
        /// </summary>
        /// <param name="callbackQuery"></param>
        private async Task HandleCallbackQuery(CallbackQuery callbackQuery)
        {
            var socialLinks = await _dataApi.GetSocialLinks();
            var data = callbackQuery.Data;
            var services = await _dataApi.GetServices();
            var projects = await _dataApi.GetProjects();
            var blogs = await _dataApi.GetBlogs();

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

        private async Task SendProjects(Project[] projects, long id)
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

            last = await _botClient.SendTextMessageAsync(id, msg,
                    replyMarkup: replyMarkup);
        }

        private async Task SendServices(Service[] services, long id)
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

            last = await _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: replyMarkup);
        }

        private async Task SendBlogs(Blog[] blogs, long id)
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

            last = await _botClient.SendTextMessageAsync(id, msg,
                replyMarkup: replyMarkup);
        }

        private async Task SendSocialLinks(SocialLinks links, long id)
        {
            var msg = $"Youtube: {links.Youtube}\nTelegram: {links.Telegram}\nVK: {links.Vk}";
            last = await _botClient.SendTextMessageAsync(id, msg,
                    disableWebPagePreview: true,
                    replyMarkup: menu);
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
