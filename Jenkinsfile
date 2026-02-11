pipeline {
    agent any

    environment {
        // .NET
        DOTNET_ROOT = 'C:\\Program Files\\dotnet'

        // Solution
        SOLUTION_NAME = 'practise1.sln'

        // SonarQube
        SONAR_PROJECT_KEY = 'practise1'
        SONAR_SCANNER_NAME = 'SonarScanner for MSBuild'
    }

    stages {

        stage('Checkout') {
            steps {
                deleteDir()
                checkout scm
            }
        }

        stage('SonarQube + Build + Test') {
            steps {
                script {
                    def scannerHome = tool SONAR_SCANNER_NAME

                    withSonarQubeEnv('MySonarQube') {

                        // ---- SONAR BEGIN ----
                        bat """
                        "${scannerHome}\\SonarScanner.MSBuild.exe" begin ^
                          /k:"${SONAR_PROJECT_KEY}" ^
                          /d:sonar.cs.opencover.reportsPaths=TestResults/**/coverage.opencover.xml
                        """

                        // ---- BUILD ----
                        bat "dotnet build \"${SOLUTION_NAME}\" --configuration Debug"

                        // ---- TEST WITH COVERAGE ----
                        bat """
                        dotnet test \"${SOLUTION_NAME}\" ^
                          --no-build ^
                          --collect:\"XPlat Code Coverage\" ^
                          --results-directory TestResults ^
                          -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
                        """

                        // ---- SONAR END ----
                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
                    }
                }
            }
        }

        stage('Quality Gate') {
            steps {
                timeout(time: 5, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing application...'
                bat 'dotnet publish -c Release -o publish'
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully'
        }
        failure {
            echo 'Pipeline failed'
        }
        always {
            echo 'Pipeline execution finished'
        }
    }
}
