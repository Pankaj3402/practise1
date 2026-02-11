pipeline {
    agent any

    options {
        skipDefaultCheckout(true)
    }

    environment {
        DOTNET_ROOT = 'C:\\Program Files\\dotnet'
        SOLUTION_NAME = 'practise1.sln'
        SONAR_PROJECT_KEY = 'practise1'
        SONAR_SCANNER_NAME = 'SonarScanner for MSBuild'
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('SonarQube + Build + Test') {
            steps {
                script {
                    def scannerHome = tool SONAR_SCANNER_NAME

                    withSonarQubeEnv('MySonarQube') {

                        bat """
                        "${scannerHome}\\SonarScanner.MSBuild.exe" begin ^
                        /k:"${SONAR_PROJECT_KEY}"
                        """

                        bat "dotnet build \"${SOLUTION_NAME}\""

                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
                    }
                }
            }
        }
    }
}
