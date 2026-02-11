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

                        // 🔹 SONAR BEGIN
                        bat """
                        "${scannerHome}\\SonarScanner.MSBuild.exe" begin ^
                          /k:"${SONAR_PROJECT_KEY}" ^
                          /d:sonar.cs.opencover.reportsPaths=TestResults/**/coverage.opencover.xml ^
                          /d:sonar.cs.vstest.reportsPaths=TestResults/**/*.trx
                        """

                        // 🔹 BUILD
                        bat "dotnet build \"${SOLUTION_NAME}\" --configuration Debug"

                        // 🔹 TEST + COVERAGE
                        bat """
                        dotnet test \"${SOLUTION_NAME}\" ^
                          --no-build ^
                          --logger trx ^
                          --results-directory TestResults ^
                          --collect:\"XPlat Code Coverage\" ^
                          -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
                        """

                        // 🔹 SONAR END
                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
                    }
                }
            }
        }

        stage('Quality Gate') {
            steps {
                timeout(time: 10, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }
    }

    post {
        success {
            echo '✅ Pipeline completed successfully'
        }
        failure {
            echo '❌ Pipeline failed'
        }
        always {
            echo '📦 Pipeline execution finished'
        }
    }
}
