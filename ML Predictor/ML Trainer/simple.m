function [result] = simple(X, Y, m, n)
  theta_t = zeros(n+1, 1);
  lambda = 0.1;
  %train model
  options = optimset('GradObj', 'on', 'MaxIter', 50);
  all_theta = zeros(num_labels, n+1);
  for c = -5:5
    [theta]=fmincg(@(t)(lrCostFunction(t, X, (Y > c).*(Y <= (c+1)), lambda)), theta_t,  options);
    all_theta(c+6,:) = theta;
  endfor 



  %% ================ Part 3: Predict for One-Vs-All ================
  disp(all_theta);
  pred = predictOneVsAll(all_theta, X);
  result = mean(double(Y > pred').*double(Y <= (pred'.+1))) * 100
  fprintf('\nTraining Set Accuracy: %f\n', result );
 end