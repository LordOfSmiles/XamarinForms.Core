namespace XamarinForms.Core.PlatformServices;

public interface IPlatformBackgroundTaskService
{
    /// <summary>
    /// выполнение действия на уровне платформы, которое не прервется при завершении приложения
    /// </summary>
    /// <param name="action"></param>
    void RunBackgroundTask(Action action);

    /// <summary>
    /// выполнение действия на уровне платформы, которое не прервется при завершении приложения
    /// </summary>
    /// <param name="action"></param>
    void RunBackgroundTask(Func<Task> action);
}