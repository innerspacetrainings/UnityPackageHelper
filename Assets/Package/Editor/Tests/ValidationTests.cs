using DTValidator;
using DTValidator.ValidationErrors;
using NUnit.Framework;

namespace UnityPackageHelper.Editor.Tests
{
    public static class ValidationTests
    {
        [Test]
        public static void ValidateSavedScriptableObjects()
        {
            var errors = ValidationUtil.ValidateAllSavedScriptableObjects(true);
            Assert.That(errors, Is.Empty);
        }

        [Test]
        public static void ValidateGameObjectsInResources()
        {
            var errors = ValidationUtil.ValidateAllGameObjectsInResources(true);
            Assert.That(errors, Is.Empty);
        }

        [Test]
        public static void ValidateBuildScenes()
        {
            var errors =
                ValidationUtil.ValidateAllGameObjectsInBuildSettingScenes(true);
            var failedComponents = string.Empty;
            var anotherFailedObjects = string.Empty;
            foreach (var validationError in errors)
            {
                var comp = validationError as ComponentValidationError;
                if (comp != null)
                {
                    failedComponents += comp.ComponentPath + "/n";
                }
                else
                {
                    anotherFailedObjects += comp + "/n";
                }
            }

            Assert.That(errors, Is.Empty, $"Errors={errors.Count}\n" +
                                          $"FailedComponents: {failedComponents} \n" +
                                          $"Objects: {anotherFailedObjects}");
        }
    }
}